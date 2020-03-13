using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CarProductLine
{
    public class Exponential_Backoff
    {
        // Enum representing the back-off strategy to use. Required parameter for DoActionWithRetry()
        public enum BackOffStrategy
        {
            Linear = 1,
            Exponential = 2
        }

        // Retry a specific codeblock wrapped in an Action delegate
        public void DoActionWithRetry(Action action, int maxRetries, int waitBetweenRetrySec, BackOffStrategy retryStrategy)
        {
            if (action == null)
            {
                throw new ArgumentNullException("No action specified");
            }

            int retryCount = 0;
            while (retryCount < maxRetries)
            {
                try
                {
                    action();
                    break;
                }
                catch (Exception ex)
                {
                    if (retryCount == maxRetries - 1)
                    {
                        DisplayError(ex);
                        throw ex;
                    }
                    else
                    {
                        //Maybe Log the number of retries
                        DisplayError(ex);

                        TimeSpan sleepTime;
                        if (retryStrategy == BackOffStrategy.Linear)
                        {
                            //Wait time is Fixed
                            sleepTime = TimeSpan.FromSeconds(waitBetweenRetrySec);
                        }
                        else
                        {
                            //Wait time increases exponentially
                            sleepTime = TimeSpan.FromSeconds(Math.Pow(waitBetweenRetrySec, retryCount));
                        }

                        Thread.Sleep(sleepTime);

                        retryCount++;
                    }
                }
            }
        }

        private void DisplayError(Exception ex)
        {
            Console.WriteLine("Encountered exception {0}, retrying operation", ex.Message);
        }
    }
}
