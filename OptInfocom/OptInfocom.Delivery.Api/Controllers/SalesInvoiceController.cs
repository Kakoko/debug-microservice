using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OptInfocom.Delivery.Api.Authorization;
using OptInfocom.Delivery.Api.Formatting;
using OptInfocom.Delivery.Application.Interfaces;
using System.Net;

namespace OptInfocom.Delivery.Api.Controllers
{
    [ApiAuthorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    [ApiVersion("1.0")]
    [ApiController]
    public class SalesInvoiceController : ControllerBase
    {
        public readonly ISalesInvoiceService _salesInvoiceService;
        public SalesInvoiceController(ISalesInvoiceService salesInvoiceService)
        {
            _salesInvoiceService = salesInvoiceService;
        }

        #region(v1.0 ===================================================================)

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("id/{id}", Name = nameof(GetInvoiceByID_V1))]
        public async Task<IActionResult> GetInvoiceByID_V1(int id, CancellationToken cancellationToken = default)
        {
            var result = await _salesInvoiceService.GetByInvoiceIDAsync(id, cancellationToken);
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

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("code/{code}", Name = nameof(GetInvoiceByCode_V1))]
        public async Task<IActionResult> GetInvoiceByCode_V1(string code, CancellationToken cancellationToken = default)
        {
            var result = await _salesInvoiceService.GetByInvoiceCodeAsync(code, cancellationToken);
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
