using OptInfocom.Item.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptInfocom.Item.Application.Interfaces
{
    public interface IApiService
    {
        Task<TResponse> SendRequestAsync<TRequest, TResponse>(
        HttpMethod method,
        string endpointUrl,
        TRequest requestBody = default)
        where TRequest : class
        where TResponse : class;
    }
}
