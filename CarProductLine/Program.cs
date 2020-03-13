using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CarProductLine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Set general variables
            int carsRequested = int.Parse(ConfigurationManager.AppSettings["carsRequested"]);

            CarFactory carFactory = new CarFactory();
            DashboadInformation dashboadInformation = new DashboadInformation();

            dashboadInformation.printLine("Requesting {0} cars", carsRequested);

            for (int i = 0; i < carsRequested; i++)
            {
                //Request car
                Task<Car> carTask = carFactory.CreateCarAsync();

                //--------------------------------Request Parts--------------------------------
                //Create wheels (4)
                Task<int> wheelsTask = carFactory.GenerateWheelsAsync(4);

                //Create chassis
                Task<Object> chassisTask = carFactory.GenerateChassisAsync();

                //Create engine
                Task<string> engineTask = carFactory.GenerateEngineAsync("1.8-Liter 4-Cylinder DOHC 16-Valve with Dual Variable Valve Timing with intelligence (VVT-i); 139 hp @ 6100 rpm; 126 lb.-ft. @ 3900 rpm");

                //Create transmission
                Task<int> transmissionTask = carFactory.GenerateTransmissionAsync(5);

                //Create model
                Task<string> modelTask = carFactory.GenerateModelAsync("Corolla 2020 LE");

                //Create interiors
                Task<string> interiorTask = carFactory.GenerateInteriorAsync("Color Brown");

                //Create electrics
                Task<string> electricsTask = carFactory.GenerateElectricsAsync("24v");

                //Create windows
                Task<int> windowsTask = carFactory.GenerateWindowsAsync(6);

                //--------------------------------Assembly line--------------------------------
                //Assembly 
                Car car = carTask.Result;
                dashboadInformation.printLine("Waiting for parts...");
                waitingParts(wheelsTask, chassisTask, engineTask, transmissionTask, modelTask, interiorTask, electricsTask, windowsTask);

                Task<Car> assemblyCarTask = carFactory.assemblyCar(car, wheelsTask.Result, chassisTask.Result, engineTask.Result, transmissionTask.Result, modelTask.Result, interiorTask.Result, electricsTask.Result, windowsTask.Result);
                dashboadInformation.printLine("Assembling cars");
                assemblyCarTask.Wait();

                //--------------------------------Testing--------------------------------------
                //Car testing and validation
                try
                {
                    Task<bool> resultValidationTask = carFactory.carValidation(assemblyCarTask.Result);
                    dashboadInformation.printLine("Validating car...");
                    resultValidationTask.Wait();
                    dashboadInformation.printLine("Car validation result: {0} for car {1}", resultValidationTask.Result, car.id);

                    try
                    {
                        new Exponential_Backoff().DoActionWithRetry(() =>
                        {
                            //Error simulation in the validation area
                            if (new Random().Next(1, 3) == 1)
                            {
                                throw new Exception("Error Simulation Example...\n");
                            }
                        }, 3, 1, Exponential_Backoff.BackOffStrategy.Exponential);
                    }
                    catch (Exception ex)
                    {
                        //At this point you can either log the error or log the error and rethrow the exception, depending on your requirements
                        Console.WriteLine("Exhausted all retries - exiting program");
                        throw ex;
                    }

                    String carInformation = @"
            Car Information: 
                id: {0},
                wheels: {1},
                chassis: {2},
                engine: {3},
                transmission: {4},
                model: {5},
                interior: {6},
                electrics: {7},
                windows: {8}
                ";
                    dashboadInformation.printLine(carInformation, car.id, car.wheels, car.chassis, car.engine, car.transmission, car.model, car.interior, car.electrics, car.windows);

                    //Delivery
                    dashboadInformation.printLine("Sending the vehicle {0} to the client.", car.id);
                }
                catch (Exception e)
                {
                    dashboadInformation.printLine(e.Message);
                    break;
                }
            }

            dashboadInformation.printLine("Program Finished");
            Console.ReadLine();
        }

        private static void waitingParts(Task<int> wheelsTask, Task<object> chassisTask, Task<string> engineTask, Task<int> transmissionTask, Task<string> modelTask, Task<string> interiorTask, Task<string> electricsTask, Task<int> windowsTask)
        {
            Task t = Task.WhenAll(wheelsTask, chassisTask, engineTask, transmissionTask, modelTask, interiorTask, electricsTask, windowsTask);
            t.Wait();
        }
    }
}
