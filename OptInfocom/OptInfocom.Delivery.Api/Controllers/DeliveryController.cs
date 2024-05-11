﻿using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OptInfocom.Delivery.Api.Authorization;
using OptInfocom.Delivery.Api.Formatting;
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
        public DeliveryController(IDeliveryStatusService deliveryStatusService)
        {
            //_itemService = itemService;
            _deliveryStatusService = deliveryStatusService;
        }

        #region(v1.0 ===================================================================)

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("status/id/{id}", Name = nameof(GetStatus_V1))]
        public async Task<IActionResult> GetStatus_V1(int id, CancellationToken cancellationToken = default)
        {
            //var test = await _itemService.GetByIDAsync(1);
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
    }
}