using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RestSharp.Portable;

namespace PortableClient
{
    public class ApiClient<T> where T:class
    {
        private readonly RestClient _restClient;

        public ApiClient(string apiUrl)
        {
            _restClient = new RestClient(apiUrl);
        }

        public async Task<T> GetAsync(int id)
        {
            var request = new RestRequest(id.ToString(), HttpMethod.Get);
            var response = await _restClient.Execute<T>(request);
            return response.Data;
        }

        public async Task UpdateAsync(int id, T entity)
        {
            var request = new RestRequest(id.ToString(), HttpMethod.Put);

            request.AddJsonBody(entity);

            await _restClient.Execute(request);
        }

        public async Task<List<T>> GetAllAsync()
        {
            RestRequest request = new RestRequest();

            var response = await _restClient.Execute<List<T>>(request);

            return response.Data;
        }

        public async Task<T> AddAsync(T car)
        {
            RestRequest request = new RestRequest(HttpMethod.Post);
            request.AddJsonBody(car);
            var response = await _restClient.Execute<T>(request);
            return response.Data;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var request = new RestRequest(id.ToString())
            {
                Method = HttpMethod.Delete
            };

            var response = await _restClient.Execute<T>(request);
            return response.Data;
        }
    }
}