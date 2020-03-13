using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarProductLine
{
    public class Car
    {
        DashboadInformation dashboadInformation = new DashboadInformation();

        public Car()
        {
            id = Guid.NewGuid();
            dashboadInformation.printLine("A new car {0} has been created.", id);
        }

        public Guid id { get; set; }
        public int wheels { get; set; }
        public Object chassis { get; set; }
        public string engine { get; set; }
        public int transmission { get; set; }
        public string model { get; set; }
        public string interior { get; set; }
        public string electrics { get; set; }
        public int windows { get; set; }
    }
}
