using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PortableModels;
using RestSharp.Portable;

namespace PortableClient
{
    public class CarsClient
    {
        private readonly RestClient RestClient;

        public CarsClient(string serviceMachineName)
        {
            RestClient = new RestClient($"http://{serviceMachineName}/webservice/api/mycars/");
        }

        public async Task<Car> GetCarAsync(int id)
        {
            var request = new RestRequest(id.ToString(), HttpMethod.Get);
            var response = await RestClient.Execute<Car>(request);
            return response.Data;
        }

        public async Task ModifyCarAsync(int id, Car car)
        {
            var request = new RestRequest(car.Id.ToString(), HttpMethod.Put);

            request.AddJsonBody(car);

            await RestClient.Execute(request);
        }

        public async Task<List<Car>> GetCarsAsync()
        {
            RestRequest request = new RestRequest();

            var response = await RestClient.Execute<List<Car>>(request);

            return response.Data;
        }

        public async Task<Car> AddCarAsync(Car car)
        {
            RestRequest request = new RestRequest(HttpMethod.Post);
            request.AddJsonBody(car);
            var response = await RestClient.Execute<Car>(request);
            return response.Data;
        }

        public async Task<Car> DeleteCarAsync(int id)
        {
            var request = new RestRequest(id.ToString())
            {
                Method = HttpMethod.Delete
            };

            var response = await RestClient.Execute<Car>(request);
            return response.Data;
        }
    }
}
