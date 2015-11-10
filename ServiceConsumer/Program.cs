using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using RestSharp;
using RestSharp.Extensions;

namespace ServiceConsumer
{
    class Program
    {
        static readonly RestClient Client = new RestClient("http://localhost/webservice/api/mycars/");

        static void Main(string[] args)
        {
            var addedCar = AddCar();

            foreach (var car in GetCars())
            {
                PrintCar(car);
            }


            //modify car
            ModifyCarNumber(addedCar);
            Console.WriteLine("-------------");
            PrintCar(GetCar(addedCar.Id));

            Console.WriteLine("----------------");
            var deletedCar = DeleteCar(addedCar.Id);
            PrintCar(deletedCar);

            var invalidCar = GetCar(deletedCar.Id);

            Console.ReadLine();
        }

        private static Car GetCar(int id)
        {
            var request = new RestRequest(id.ToString());
            var response = Client.Execute<Car>(request);
            return response.Data;
        }

        private static void PrintCar(Car car)
        {
            Console.WriteLine($"id: {car.Id}, Model: {car.Model}, Number: {car.Number}");
        }

        private static void ModifyCarNumber(Car addedCar)
        {
            addedCar.Number = addedCar.Number + "upadated" + DateTime.Now.Ticks;

            var request = new RestRequest(addedCar.Id.ToString());

            request.AddJsonBody(addedCar);

            Client.Put(request);
        }

        private static List<Car> GetCars()
        {
            RestRequest request = new RestRequest();

            var response = Client.Execute<List<Car>>(request);

            return response.Data;
        }

        private static Car AddCar()
        {
            RestRequest request = new RestRequest(Method.POST);
            request.AddParameter("Model", Guid.NewGuid().ToString());
            request.AddParameter("Number", Guid.NewGuid().ToString());

            var response = Client.Execute<Car>(request);

            return response.Data;
        }

        private static Car DeleteCar(int id)
        {
            var request = new RestRequest(id.ToString())
            {
                Method = Method.DELETE
            };

            var response = Client.Execute<Car>(request);
            return response.Data;
        }
    }
}
