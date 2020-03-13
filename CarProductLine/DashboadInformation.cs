using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarProductLine
{
    public class DashboadInformation
    {
        public void printLine(String message, params object[] args)
        {
            Console.WriteLine(String.Format(message, args));
        }
    }
}
