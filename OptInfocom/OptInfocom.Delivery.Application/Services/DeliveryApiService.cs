﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OptInfocom.Item.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptInfocom.Item.Application.Services
{
    public class DeliveryApiService : IDeliveryApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
      //  private readonly IDictionary<string, string> _defaultHeaders;

        public DeliveryApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetSection("BaseUrl").Value; 
            _httpClient.BaseAddress = new Uri(_baseUrl);
           
        }

        public async Task<TResponse> SendRequestAsync<TRequest, TResponse>(
            HttpMethod method,
            string endpointUrl,
            TRequest requestBody = default,
            IDictionary<string, string> additionalHeaders = null)
            where TRequest : class
            where TResponse : class
        {
     
            using var request = new HttpRequestMessage(method, new Uri(_baseUrl + endpointUrl));

           

            // Add additional headers (if provided)
            if (additionalHeaders != null)
            {
                foreach (var header in additionalHeaders)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

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
