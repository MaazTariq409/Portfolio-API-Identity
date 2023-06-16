using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Controllers
{
    [Route("api/userproduct")]
    [Authorize]
    [ApiController]
    public class UserProductsController : ControllerBase
    {
        private readonly IProducts _userProductsRepository;
        private readonly IMapper _mapper;
        private ResponseObject _responseObject;
        private readonly ILogger<UserProductsController> _logger;

        public UserProductsController( IMapper mapper, IProducts userProductsRepository, ILogger<UserProductsController> logger)
        {
            _mapper = mapper;
            _userProductsRepository = userProductsRepository;
            _logger = logger;
        }


        //Get
        [AllowAnonymous]
        [HttpGet("/api/userproducts")]

        public ActionResult<List<UserProductsDto>> GetAllProducts()
        {
            try
            {
                var productsFromDb = _userProductsRepository.GetProducts();

                if (productsFromDb == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "No Product is found");
                    return NotFound(_responseObject);
                }

                var approvedProducts = productsFromDb.Where(x => x.Status != "pending").ToList();

                if (approvedProducts.Count() == 0)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Products are waiting for Approval");
                    return NotFound(_responseObject);
                }

                UserProductsDto[] userProducts = new UserProductsDto[approvedProducts.Count()];

                for (int i = 0; i < userProducts.Count(); i++)
                {
                    userProducts[i] = new UserProductsDto();

                    userProducts[i].Title = approvedProducts[i].Title;
                    userProducts[i].Description = approvedProducts[i].Description;
                    userProducts[i].PermaLink = approvedProducts[i].PermaLink;
                    userProducts[i].DateCreated = approvedProducts[i].DateCreated;
                    userProducts[i].Type = approvedProducts[i].Type;
                    userProducts[i].Image = approvedProducts[i].Image;
                    userProducts[i].VideoUrl = approvedProducts[i].VideoUrl;

                    userProducts[i].ProfileUrl = approvedProducts[i].UserProfile.ProfileUrl;
                    userProducts[i].Name = approvedProducts[i].UserProfile.Name;
                    userProducts[i].Email = approvedProducts[i].UserProfile.Email;
                    userProducts[i].linkedin = approvedProducts[i].UserProfile.Linkedin;
                    userProducts[i].Github = approvedProducts[i].UserProfile.Github;
                }

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", userProducts);


                return Ok(_responseObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving products.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while retrieving products.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }

           
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<UserProductsDto>> GetUserProducts()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "User Not Found");
                    return NotFound(_responseObject);
                }
                var productsFromDb = _userProductsRepository.GetProductsByUserID(userId);

                if (productsFromDb == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "No Product is found");
                    return NotFound(_responseObject);
                }

                var approvedProducts = productsFromDb.Where(x => x.Status != "pending").ToList();

                if (approvedProducts.Count() == 0)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Products are waiting for Approval");
                    return NotFound(_responseObject);
                }

                UserProductsDto[] userProducts = new UserProductsDto[approvedProducts.Count()];

                for (int i = 0; i < userProducts.Count(); i++)
                {
                    userProducts[i] = new UserProductsDto();

                    userProducts[i].Title = approvedProducts[i].Title;
                    userProducts[i].Description = approvedProducts[i].Description;
                    userProducts[i].PermaLink = approvedProducts[i].PermaLink;
                    userProducts[i].DateCreated = approvedProducts[i].DateCreated;
                    userProducts[i].Type = approvedProducts[i].Type;
                    userProducts[i].Image = approvedProducts[i].Image;
                    userProducts[i].VideoUrl = approvedProducts[i].VideoUrl;

                    userProducts[i].ProfileUrl = approvedProducts[i].UserProfile.ProfileUrl;
                    userProducts[i].Name = approvedProducts[i].UserProfile.Name;
                    userProducts[i].Email = approvedProducts[i].UserProfile.Email;
                    userProducts[i].linkedin = approvedProducts[i].UserProfile.Linkedin;
                    userProducts[i].Github = approvedProducts[i].UserProfile.Github;
                }

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", userProducts);


                return Ok(_responseObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user products.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while retrieving user products.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
           
        }

        //POST 
        [HttpPost]
        public ActionResult AddUserProduct([FromBody] ProductsDto userProducts)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "User Not Found");
                    return NotFound(_responseObject);
                }

                var addProducts = _mapper.Map<UserProducts>(userProducts);

                _userProductsRepository.AddProductsByUserID(userId, addProducts);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "User Product Added Succesfully");

                return Ok(_responseObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding user products.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while adding user products.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
           

        }


        [HttpPut("{productsId}")]
        public ActionResult UpdateUserProducts(int productsId, [FromBody] ProductsDto userProducts)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "User Not Found");
                    return NotFound(_responseObject);
                }

                var updateUserProducts = _mapper.Map<UserProducts>(userProducts);

                _userProductsRepository.updateProductsByUserID(userId, productsId, updateUserProducts);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "User Updated Updated succesfully");

                return Ok(_responseObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating user products.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while updating user products.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
          

        }


        [HttpDelete("{productsId}")]
        public IActionResult DeleteUserProducts(int productsId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;

                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "User Not Found");
                    return NotFound(_responseObject);
                }

                _userProductsRepository.removeProductsByUserID(userId, productsId);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "User Product Deleted successfully");

                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting user products.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while deleting user products.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
          

        }
    }
}
