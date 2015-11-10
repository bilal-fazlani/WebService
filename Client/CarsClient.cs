using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Models;
using RestSharp;
using RestSharp.Serializers;

namespace Client
{
    public class CarsClient
    {
        static readonly RestClient RestClient = 
            new RestClient($"http://{Environment.MachineName}/webservice/api/mycars/");

        public Car GetCar(int id)
        {
            var request = new RestRequest(id.ToString());
            var response = RestClient.Execute<Car>(request);
            return response.Data;
        }

        public async Task<Car> GetCarAsync(int id)
        {
            var request = new RestRequest(id.ToString());
            var response = await RestClient.ExecuteTaskAsync<Car>(request);
            return response.Data;
        }

        public void ModifyCar(int id, Car car)
        {
            var request = new RestRequest(car.Id.ToString());

            request.AddJsonBody(car);

            RestClient.Put(request);
        }

        public List<Car> GetCars()
        {
            RestRequest request = new RestRequest();

            var response = RestClient.Execute<List<Car>>(request);

            return response.Data;
        }

        public async Task<List<Car>> GetCarsAsync()
        {
            RestRequest request = new RestRequest();

            var response = await RestClient.ExecuteTaskAsync<List<Car>>(request);

            return response.Data;
        }

        public Car AddCar(Car car)
        {
            RestRequest request = new RestRequest(Method.POST);

            request.AddJsonBody(car);

            var response = RestClient.Execute<Car>(request);

            return response.Data;
        }

        public async Task<Car> AddCarAsync(Car car)
        {
            RestRequest request = new RestRequest(Method.POST);

            request.AddJsonBody(car);

            var response = await RestClient.ExecuteTaskAsync<Car>(request);

            return response.Data;
        }

        public Car DeleteCar(int id)
        {
            var request = new RestRequest(id.ToString())
            {
                Method = Method.DELETE
            };

            var response = RestClient.Execute<Car>(request);
            return response.Data;
        }

        public async Task<Car> DeleteCarAsync(int id)
        {
            var request = new RestRequest(id.ToString())
            {
                Method = Method.DELETE
            };

            var response = await RestClient.ExecuteTaskAsync<Car>(request);
            return response.Data;
        }
    }
}
