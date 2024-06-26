﻿using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OptInfocom.Delivery.Api.Authorization;
using OptInfocom.Delivery.Api.Formatting;
using OptInfocom.Delivery.Api.Models;
using OptInfocom.Delivery.Application.Interfaces;
using OptInfocom.Delivery.Domain.Models;
using OptInfocom.Item.Application.Interfaces;
using System.Net;

namespace OptInfocom.Delivery.Api.Controllers
{
    //[ApiAuthorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        //public readonly IItemService _itemService;
        public readonly IDeliveryStatusService _deliveryStatusService;
        public readonly IDeliveryApiService _deliveryApiService;
        public DeliveryController(IDeliveryStatusService deliveryStatusService, IDeliveryApiService deliveryApiService)
        {
            //_itemService = itemService;
            _deliveryStatusService = deliveryStatusService;
            _deliveryApiService = deliveryApiService;
        }

        #region(v1.0 ===================================================================)

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("status/id/{id}", Name = nameof(GetStatus_V1))]
        public async Task<IActionResult> GetStatus_V1(int id, CancellationToken cancellationToken = default)
        {
            // Extract headers from the request
            var authorizationHeader = HttpContext.Request.Headers["Authorization"];
            var appIdHeader = HttpContext.Request.Headers["appid"];
            var appKeyHeader = HttpContext.Request.Headers["appkey"];

            // Make an API call to Item API to Get By ID
            var headers = new Dictionary<string, string>
            {
                    { "Authorization", authorizationHeader },
                    { "appid", appIdHeader },
                    { "appkey", appKeyHeader }
            };

            //When you request for Item Data
            //Do you care about how the data is returned?
            // Do you just want to get the data?
            var resultFromItemApi = await _deliveryApiService.SendRequestAsync<object, ItemResponse>(HttpMethod.Get, $"api/v1/Items/id/{id}", null, headers);

            var result = await _deliveryStatusService.GetByInvoiceIDAsync(id, cancellationToken);
            ResponseFormatter<object> response = new ResponseFormatter<object>()
            {
                code = (int)HttpStatusCode.OK,
                success = true,
                message = "delivery status",
                data = new { resultSet = result },
                pagination = null
            };
            return Ok(response);
        }

        //For New Developer all they do is create a new solution for their
        //microservice and start coding. You will agree on the endpoint
        //to give you the data you need


        //For the IoC , that will take some time to rearrange the code

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("status/insert", Name = nameof(PostStatus_V1))]
        public async Task<IActionResult> PostStatus_V1(DeliveryStatus entity, CancellationToken cancellationToken = default)
        {
            entity.unq_code = Guid.NewGuid().ToString().ToUpper();
            var result = await _deliveryStatusService.SaveAsync(entity, cancellationToken);
            ResponseFormatter<object> response = new ResponseFormatter<object>()
            {
                code = (int)HttpStatusCode.OK,
                success = true,
                message = "new delivery status submitted",
                data = new { resultSet = result },
                pagination = null
            };
            return Ok(response);
        }

        #endregion

        #region(v2.0 ===================================================================)

        [HttpGet]
        [MapToApiVersion("2.0")]
        [Route("status/id/{id}", Name = nameof(GetStatus_V2))]
        public async Task<IActionResult> GetStatus_V2(int id, CancellationToken cancellationToken = default)
        {
            var result = _deliveryStatusService.GetByInvoiceIDAsync(id, cancellationToken);
            ResponseFormatter<object> response = new ResponseFormatter<object>()
            {
                code = (int)HttpStatusCode.OK,
                success = true,
                message = "delivery status",
                data = new { resultSet = result },
                pagination = null
            };
            return Ok(response);
        }

        #endregion

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("consume-item")]
        public async Task<IActionResult> ConsumeItem()
        {
            var headers = new Dictionary<string, string>
            {
                    { "authorization", "Bearer your_access_token" },
                    { "appid", "value" },
                    { "appkey", "1234EABCD-5678-4321-9F3E-DEF123456789" }
                   
            };
            var result =  await  _deliveryApiService.SendRequestAsync<object , ItemResponse>(HttpMethod.Get, $"api/v1/Items/all", null , headers);
           
    
            ResponseFormatter<object> response = new ResponseFormatter<object>()
            {
                code = (int)HttpStatusCode.OK,
                success = true,
                message = "all item",
                data = new { resultSet = result },
                pagination = null
            };
            return Ok(response);
        }
    }
}
