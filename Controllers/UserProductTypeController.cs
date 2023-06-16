using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Controllers
{
    [Route("api/producttype")]
    [ApiController]
    public class UserProductTypeController : ControllerBase
    {
        private readonly IProductType _productTypeRepository;
        private readonly IMapper _mapper;
        private ResponseObject _responseObject;
        private readonly ILogger<UserProductTypeController> _logger;

        public UserProductTypeController( IMapper mapper, IProductType ProductTypeRepository, ILogger<UserProductTypeController> logger)
        {
            _mapper = mapper;
            _productTypeRepository = ProductTypeRepository;
            _logger = logger;
        }
        [HttpGet]
        public ActionResult<List<UserProductType>> GetProductTypes()
        {
            try
            {
                var ProductTypeFromDb = _productTypeRepository.GetUserProductTypes();

                if (ProductTypeFromDb == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "No ProductType Exists is Empty");
                    return NotFound(_responseObject);
                }

                var productTypesDto = ProductTypeFromDb;

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", productTypesDto);


                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving product types.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while retrieving product types.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }

            

        }


        [HttpPost]
        public ActionResult AddProductType([FromBody] UserProductType productType)
        {
            try
            {
                if (productType == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Can not add Null Value");
                    return NotFound(_responseObject);
                }

                var productTType = new UserProductType();
                productTType.ProductName = productType.ProductName;

                _productTypeRepository.AddProductType(productTType);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Product Type Added Succesfully");

                return Ok(_responseObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding product type.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while adding product type.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
           

        }


        [HttpPut("{productTypeId}")]
        public ActionResult UpdateProductType(int productTypeId, [FromBody] UserProductType productType)
        {
            try
            {
                var updateProductType = productType;

                _productTypeRepository.UpdateProductType(productTypeId, updateProductType);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Product type Updated succesfully");

                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating product type.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while updating product type.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }


           

        }


        [HttpDelete("{productTypeId}")]
        public IActionResult DeleteProductType(int productTypeId)
        {
            try
            {
                _productTypeRepository.RemoveProductType(productTypeId);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Product Type Deleted successfully");

                return Ok(_responseObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting product type.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while deleting product types.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }

            //if (cityId == 0)
            //{
            //    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
            //    return NotFound(_responseObject);
            //}

          

        }
    }
}
