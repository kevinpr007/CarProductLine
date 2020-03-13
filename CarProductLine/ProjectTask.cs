using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;

namespace CarProductLine
{
    public class ProjectTask
    {
        private RequestTime requestTime = new RequestTime();
        DashboadInformation dashboadInformation = new DashboadInformation();

        private readonly string minimumValue = ConfigurationManager.AppSettings["minimumTimeForAsyncTask"];
        private readonly string maximumValue = ConfigurationManager.AppSettings["maximumTimeForAsyncTask"];

        public async Task GetHeavyTaskSync(string value)
        {
            int time = requestTime.GetRandomNumber(Int32.Parse(minimumValue), Int32.Parse(maximumValue));
            await Task.Delay(time);
            dashboadInformation.printLine("**Time spent on task {0}: {1}ms", value, time);
        }
    }
}
