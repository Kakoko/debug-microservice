using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OptInfocom.Item.Api.Model;
using OptInfocom.Item.Application.Interfaces;
using OptInfocom.Item.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptInfocom.Item.Application.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetSection("BaseUrl").Value; // Get base URL directly
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<TResponse> SendRequestAsync<TRequest, TResponse>(
            HttpMethod method,
            string endpointUrl,
            TRequest requestBody = default)
            where TRequest : class
            where TResponse : class
        {
     
            using var request = new HttpRequestMessage(method, new Uri(_baseUrl + endpointUrl));


            if (requestBody != null)
            {
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(requestBody),
                    Encoding.UTF8,
                    "application/json"); // Replace with appropriate content type
            }

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                var responseData = JObject.Parse(responseContent)["data"]["resultSet"].ToString();
                var resultSet = JsonConvert.DeserializeObject<List<TResponse>>(responseData);

                
                return resultSet.FirstOrDefault();
            }
            else
            {
                // Handle error cases (throw exception, log error)
                throw new Exception($"API request failed: {response.StatusCode}");
            }
        }
    }
}
