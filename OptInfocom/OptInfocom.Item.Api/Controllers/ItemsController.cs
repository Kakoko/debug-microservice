using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Nest;
using OptInfocom.Item.Api.Authorization;
using OptInfocom.Item.Api.Formatting;
using OptInfocom.Item.Application.Interfaces;
using OptInfocom.Item.Domain.Models;
using System.Net;

namespace OptInfocom.Item.Api.Controllers
{
    [ApiAuthorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        public readonly IMasterDatabaseService _databaseService;
        public readonly IItemService _itemService;
        private readonly IElasticClient _elasticClient;

        public ItemsController(IMasterDatabaseService databaseService, IItemService itemService, IElasticClient elasticClient)
        {
            _databaseService = databaseService;
            _itemService = itemService;
            _elasticClient = elasticClient;
        }

        #region(v1.0 ===================================================================)

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("id/{id}", Name = nameof(GetByID_V1))]
        public async Task<IActionResult> GetByID_V1(int id, CancellationToken cancellationToken = default)
        {
            var result = await _itemService.GetByIDAsync(id, cancellationToken);
            ResponseFormatter<object> response = new ResponseFormatter<object>()
            {
                code = (int)HttpStatusCode.OK,
                success = true,
                message = "item by id",
                data = new { resultSet = result },
                pagination = null
            };
            return Ok(response);
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("name/{name}", Name = nameof(GetByName_V1))]
        public async Task<IActionResult> GetByName_V1(string name, CancellationToken cancellationToken = default)
        {
            var result = _elasticClient.SearchAsync<ItemMaster>(
               s => s.Query(q => q.Match(m => m.Field(m => m.item_name).Query(name))).Size(5000));

            var finalResult = result;
            var searchedItemList = finalResult.Result.Documents.ToList();

            if (searchedItemList == null || searchedItemList?.Count() == 0)
            {
                var resultItem = await _itemService.GetByNameAsync(name, cancellationToken);
                ResponseFormatter<object> response = new ResponseFormatter<object>()
                {
                    code = (int)HttpStatusCode.NotFound,
                    success = true,
                    message = "search by item name",
                    data = new { resultSet = resultItem },
                    pagination = null
                };
                return Ok(response);
            }
            else
            {
                ResponseFormatter<object> response = new ResponseFormatter<object>()
                {
                    code = (int)HttpStatusCode.OK,
                    success = true,
                    message = "search by item name",
                    data = new { resultSet = searchedItemList },
                    pagination = null
                };
                return Ok(response);
            }
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("all", Name = nameof(GetAllItem_V1))]
        public async Task<IActionResult> GetAllItem_V1(CancellationToken cancellationToken = default)
        {
            var result = await _itemService.GetAllAsync(cancellationToken);
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

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("update/index", Name = nameof(UpdateItemIndex_V1))]
        public async Task<IActionResult> UpdateItemIndex_V1(CancellationToken cancellationToken = default)
        {
            try
            {
                int insert = 0;
                int update = 0;
                IndexResponse result = null;

                var ItemList = await _itemService.GetAllAsync(cancellationToken);
                foreach (var item in ItemList)
                {
                    var data = _elasticClient.SearchAsync<ItemMaster>(
                        s => s.Query(q => q.Match(m => m.Field(m => m.item_id).Query(item.item_id.ToString()))).Size(5000)).Result.Documents.ToList().FirstOrDefault();
                    if (data is null)
                    {
                        result = await _elasticClient.IndexDocumentAsync(item);
                        ++insert;
                    }
                }

                ResponseFormatter<object> response = new ResponseFormatter<object>()
                {
                    code = (int)HttpStatusCode.OK,
                    success = true,
                    message = $"item indexing done.{insert} inserted and {update} update",
                    data = new { resultSet = result },
                    pagination = null
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                ResponseFormatter<object> response = new ResponseFormatter<object>()
                {
                    code = (int)HttpStatusCode.BadRequest,
                    success = true,
                    message = "Exception:: " + ex.Message,
                    data = new { resultSet = ex.Message }
                };
                return Ok(response);
            }
        }
        #endregion
    }
}
