using System;
using PortableClient;
using PortableModels;

namespace ConsoleConsumer
{
    class Program
    {
        private static readonly ApiClient<Car> Client = new ApiClient<Car>
            ("http://BILALMUSTAF3107/webservice/api/mycars/");

        static void Main(string[] args)
        {
            var addedCar = Client.AddAsync(new Car
            {
                Number = Guid.NewGuid().ToString(),
                Model = Guid.NewGuid().ToString()
            }).Result;

            foreach (var car in Client.GetAllAsync().Result)
            {
                PrintCar(car);
            }

            //modify car


            addedCar.Number = addedCar.Number += $"_updated_{DateTime.Now.Ticks}";

            Client.UpdateAsync(addedCar.Id, addedCar).Wait();

            Console.WriteLine("-------------");

            PrintCar(Client.GetAsync(addedCar.Id).Result);

            Console.WriteLine("----------------");

            var deletedCar = Client.DeleteAsync(addedCar.Id).Result;
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
