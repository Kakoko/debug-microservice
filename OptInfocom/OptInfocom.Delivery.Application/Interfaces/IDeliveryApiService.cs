using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptInfocom.Item.Application.Interfaces
{
    public interface IDeliveryApiService
    {
        Task<TResponse> SendRequestAsync<TRequest, TResponse>(
        HttpMethod method,
        string endpointUrl,
        TRequest requestBody = default,
        IDictionary<string, string> additionalHeaders = null)
        where TRequest : class
        where TResponse : class;
    }
}
