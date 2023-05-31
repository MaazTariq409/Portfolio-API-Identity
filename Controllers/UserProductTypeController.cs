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

        public UserProductTypeController( IMapper mapper, IProductType ProductTypeRepository)
        {
            _mapper = mapper;
            _productTypeRepository = ProductTypeRepository;

        }
        [HttpGet]
        public ActionResult<List<UserProductType>> GetProductTypes()
        {
           
            var ProductTypeFromDb = _productTypeRepository.GetUserProductTypes();

            if(ProductTypeFromDb == null)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "No ProductType Exists is Empty");
                return NotFound(_responseObject);
            }

            var productTypesDto = ProductTypeFromDb;

            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", productTypesDto);


            return Ok(_responseObject);

        }


        [HttpPost]
        public ActionResult AddProductType([FromBody] UserProductType productType)
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


        [HttpPut("{productTypeId}")]
        public ActionResult UpdateProductType(int productTypeId, [FromBody] UserProductType productType)
        {
            
         

            var updateProductType = productType;

            _productTypeRepository.UpdateProductType(productTypeId, updateProductType);
            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Product type Updated succesfully");

            return Ok(_responseObject);

        }


        [HttpDelete("{productTypeId}")]
        public IActionResult DeleteProductType(int productTypeId)
        {

            //if (cityId == 0)
            //{
            //    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
            //    return NotFound(_responseObject);
            //}

            _productTypeRepository.RemoveProductType(productTypeId);
            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Product Type Deleted successfully");

            return Ok(_responseObject);

        }
    }
}
