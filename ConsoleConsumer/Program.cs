using System;
using Client;
using Models;

namespace ConsoleConsumer
{
    class Program
    {
        private static readonly CarsClient CarsClient = new CarsClient();

        static void Main(string[] args)
        {
            var addedCar = CarsClient.AddCar(new Car
            {
                Number = Guid.NewGuid().ToString(),
                Model = Guid.NewGuid().ToString()
            });

            foreach (var car in CarsClient.GetCars())
            {
                PrintCar(car);
            }

            //modify car


            addedCar.Number = addedCar.Number += $"_updated_{DateTime.Now.Ticks}";

            CarsClient.ModifyCar(addedCar.Id, addedCar);

            Console.WriteLine("-------------");

            PrintCar(CarsClient.GetCar(addedCar.Id));

            Console.WriteLine("----------------");

            var deletedCar = CarsClient.DeleteCar(addedCar.Id);
            PrintCar(deletedCar);

            var invalidCar = CarsClient.GetCar(deletedCar.Id);

            Console.ReadLine();
        }

        private static void PrintCar(Car car)
        {
            Console.WriteLine($"id: {car.Id}, Model: {car.Model}, Number: {car.Number}");
        }
    }
}
