using MaxiShop.Application.ApplicationConstants;
using MaxiShop.Application.Common;
using MaxiShop.Application.DTO.Product;
using MaxiShop.Application.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MaxiShop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        protected APIResponse _response;

        public ProductController(IProductService productService)
        {
            _productService = productService;
            _response = new();
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<APIResponse>> Create([FromBody] CreateProductDto createProductDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommonMessage.CreateOperationFailed;
                    _response.AddError(ModelState.ToString());

                    return Ok(_response);
                }
                var entity = await _productService.CreateAsync(createProductDto);
                _response.statusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommonMessage.CreateOperationSuccess;
                _response.Result = entity;
            }
            catch (Exception)
            {
                _response.statusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessage = CommonMessage.CreateOperationFailed;
                _response.AddError(CommonMessage.SystemError);
            }
            return Ok(_response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [Route("Details")]
        public async Task<ActionResult<APIResponse>> ReadAll()
        {
            try
            {
                var products = await _productService.GetAllAsync();

                _response.statusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = products;
            }
            catch (Exception)
            {
                _response.statusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommonMessage.SystemError);
            }
            return Ok(_response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<APIResponse>> Read(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);

                if (product == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.AddError(CommonMessage.RecordNotFound);

                    return Ok(_response);
                }
                _response.statusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = product;

            }
            catch (Exception)
            {
                _response.statusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommonMessage.SystemError);
            }


            return Ok(_response);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut]
        public async Task<ActionResult<APIResponse>> UpdateAsync([FromBody] UpdateProductDto updateProductDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommonMessage.UpdateOperationFailed;
                    _response.AddError(ModelState.ToString());

                    return Ok(_response);
                }
                var result = await _productService.GetByIdAsync(updateProductDto.Id);

                if (result == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommonMessage.UpdateOperationFailed;

                    return Ok(_response);
                }

                await _productService.UpdateAsync(updateProductDto);
                _response.statusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommonMessage.UpdateOperationSuccess;
            }
            catch (Exception)
            {
                _response.statusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessage = CommonMessage.UpdateOperationFailed;
                _response.AddError(CommonMessage.SystemError);
            }

            return Ok(_response);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommonMessage.DeleteOperationFailed;
                    return Ok(_response);
                }

                var result = await _productService.GetByIdAsync(id);
                if (result == null && id != 0)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommonMessage.DeleteOperationFailed;
                    return Ok(_response);
                }
                await _productService.DeleteAsync(id);
                _response.statusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommonMessage.DeleteOperationSuccess;
            }
            catch (Exception)
            {
                _response.statusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessage = CommonMessage.DeleteOperationFailed;
                _response.AddError(CommonMessage.SystemError);
            }

            return Ok(_response);
        }
    }
}
