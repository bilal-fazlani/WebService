using System;
using PortableClient;
using PortableModels;

namespace ConsoleConsumer
{
    class Program
    {
        private static readonly CarsClient CarsClient = new CarsClient("BILALMUSTAF3107");

        static void Main(string[] args)
        {
            var addedCar = CarsClient.AddCarAsync(new Car
            {
                Number = Guid.NewGuid().ToString(),
                Model = Guid.NewGuid().ToString()
            }).Result;

            foreach (var car in CarsClient.GetCarsAsync().Result)
            {
                PrintCar(car);
            }

            //modify car


            addedCar.Number = addedCar.Number += $"_updated_{DateTime.Now.Ticks}";

            CarsClient.ModifyCarAsync(addedCar.Id, addedCar).Wait();

            Console.WriteLine("-------------");

            PrintCar(CarsClient.GetCarAsync(addedCar.Id).Result);

            Console.WriteLine("----------------");

            var deletedCar = CarsClient.DeleteCarAsync(addedCar.Id).Result;
            PrintCar(deletedCar);

            //throws error because the car is deleted, hence commented
            //var invalidCar = CarsClient.GetCarAsync(deletedCar.Id).Result;

            Console.ReadLine();
        }

        private static void PrintCar(Car car)
        {
            Console.WriteLine($"id: {car.Id}, Model: {car.Model}, Number: {car.Number}");
        }
    }
}
