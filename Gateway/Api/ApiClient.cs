using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Gateway.Api
{
    public interface IApiClient<TRequest, TResponse>
    {
        Task<TResponse> Get(string relativePath, IDictionary<string, string>? queryStringParams = null);
        Task<TResponse> Post(string relativePath, TRequest request, IDictionary<string, string>? queryStringParams = null);
    }

    public class ApiClient<TRequest, TResponse> : IApiClient<TRequest, TResponse>
    {
        private readonly IHttpClientFactory _clientFactory;

        public ApiClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<TResponse> Get(string relativePath, IDictionary<string, string>? queryStringParams = null)
        {
            var client = _clientFactory.CreateClient(Constants.HttpClients.Users);

            var endpoint = GetEndpoint(relativePath, queryStringParams);
            var message = new HttpRequestMessage(HttpMethod.Get, endpoint);

            var httpResponse = await client.SendAsync(message);
            var response = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
                throw new ApiClientException(httpResponse.StatusCode, response);

            return JsonConvert.DeserializeObject<TResponse>(response) ?? throw new Exception($"Could not deserialize object of type {typeof(TResponse).Name}");
        }

        public async Task<TResponse> Post(string relativePath, TRequest request, IDictionary<string, string>? queryStringParams = null)
        {
            var client = _clientFactory.CreateClient(Constants.HttpClients.Users);

            var endpoint = GetEndpoint(relativePath, queryStringParams);
            var message = new HttpRequestMessage(HttpMethod.Post, endpoint);

            var json = JsonConvert.SerializeObject(request);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponse = await client.SendAsync(message);
            var response = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
                throw new ApiClientException(httpResponse.StatusCode, response);

            return JsonConvert.DeserializeObject<TResponse>(response) ?? throw new Exception($"Could not deserialize object of type {typeof(TResponse).Name}");
        }

        private Uri GetEndpoint(string relativePath, IDictionary<string, string>? queryStringParams)
        {
            var endpoint = new Uri(relativePath);

            if (queryStringParams is null)
                return endpoint;

            var uriBuilder = new UriBuilder(endpoint);

            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            foreach (var param in queryStringParams)
                query[param.Key] = param.Value;

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }
    }
}
