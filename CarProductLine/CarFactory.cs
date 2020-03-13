using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CarProductLine
{
    public class CarFactory
    {
        private ProjectTask projectTask = new ProjectTask();

        public async Task<Car> CreateCarAsync()
        {
            await projectTask.GetHeavyTaskSync("CreateCarAsync");
            return new Car();
        }

        public async Task<int> GenerateWheelsAsync(int value)
        {
            await projectTask.GetHeavyTaskSync("GenerateWheelsAsync");
            return value;
        }

        public async Task<object> GenerateChassisAsync()
        {
            await projectTask.GetHeavyTaskSync("GenerateChassisAsync");
            return new Object();
        }

        public async Task<string> GenerateEngineAsync(string value)
        {
            await projectTask.GetHeavyTaskSync("GenerateEngineAsync");
            return value;
        }

        public async Task<int> GenerateTransmissionAsync(int value)
        {
            await projectTask.GetHeavyTaskSync("GenerateTransmissionAsync");
            return value;
        }

        public async Task<string> GenerateModelAsync(string value)
        {
            await projectTask.GetHeavyTaskSync("GenerateModelAsync");
            return value;
        }

        public async Task<string> GenerateInteriorAsync(string value)
        {
            await projectTask.GetHeavyTaskSync("GenerateInteriorAsync");
            return value;
        }

        public async Task<string> GenerateElectricsAsync(string value)
        {
            await projectTask.GetHeavyTaskSync("GenerateElectricsAsync");
            return value;
        }

        public async Task<int> GenerateWindowsAsync(int value)
        {
            await projectTask.GetHeavyTaskSync("GenerateWindowsAsync");
            return value;
        }

        public async Task<Car> assemblyCar(Car car, int wheels, object chassis, string engine, int transmission, string model, string interior, string electrics, int windows)
        {
            await projectTask.GetHeavyTaskSync("assemblyCar");
            car.wheels = wheels;
            car.chassis = chassis;
            car.engine = engine;
            car.transmission = transmission;
            car.model = model;
            car.interior = interior;
            car.electrics = electrics;
            car.windows = windows;
            return car;
        }

        public async Task<bool> carValidation(Car car)
        {
            await projectTask.GetHeavyTaskSync("carValidation");
            String message = String.Empty;

            //Check a minimum wheels of 4
            if (car.wheels < (int)CarEnum.wheels)
            {
                message += "The minimum wheels must be 4.\n";
            }
            //Create engine
            if (String.IsNullOrWhiteSpace(car.engine))
            {
                message += "The engine is not set.\n";
            }
            //Create transmission
            if (car.transmission < (int)CarEnum.transmission)
            {
                message += "The transmission must have a minimum of three.\n";
            }
            //Create electrics
            if (String.IsNullOrWhiteSpace(car.electrics))
            {
                message += "The electrics must be set.\n";
            }
            //Create windows
            if (car.windows < (int)CarEnum.windows)
            {
                message += "The windows have to be set first.\n";
            }

            if (!String.IsNullOrEmpty(message))
                throw new Exception(message);

            return true;
        }
    }
}
