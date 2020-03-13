using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarProductLine
{
    public class RequestTime
    {
        //Function to get random number
        private readonly Random getRandom = new Random();

        public int GetRandomNumber(int min, int max)
        {
            lock (getRandom) // synchronize
            {
                return getRandom.Next(min, max);
            }
        }
    }
}
