using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Portfolio_API.DTOs;
using Portfolio_API.DTOs.Admin;
using Portfolio_API.Models;
using Portfolio_API.Repository;
using Portfolio_API.Repository.Repository_Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Portfolio_API.Controllers
{
    [Route("api/admin/{userId}/")]
    [ApiController]
    public class AdminApprovalController : ControllerBase
    {

        private ResponseObject _responseObject;
        private readonly IEducation _education;
        private readonly IUserExperience _experience;
        private readonly ISkills _skill;
        private readonly IProducts _product;
        private readonly IUserBlogs _userBlogs;
        private readonly IMapper _mapper;
        private readonly IUserServiceGig _service;
        private readonly ILogger<AdminApprovalController> _logger;


        public AdminApprovalController(IEducation education,IProducts product, 
            IUserExperience experience, ISkills skill, 
            IUserBlogs userBlogs, IMapper mapper, IUserServiceGig serviceGig,
            ILogger<AdminApprovalController> logger
)
        {
            _education = education;
            _experience = experience;
            _skill = skill;
            _product = product;
            _userBlogs = userBlogs;
            _mapper = mapper;
            _service = serviceGig;
            _logger = logger;

        }

        // Education Approval Endpoints

        // GET: api/<EducationController>
        [HttpGet("geteducation")]
        public ActionResult<IEnumerable<AdminGetEducation>> GetEducationDetailsAdmin(string userId)
        {
            try
            {
                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                    return NotFound(_responseObject);
                }

                var educationDetails = _education.GetDetails(userId);

                //var finalEduDetail = _mapper.Map<IEnumerable<AdminGetEducation>>(educationDetails);

                //_responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", educationDetails);

                return Ok(educationDetails);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting education details.");
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while getting education details.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);

            }
          
        }

        [HttpPut("education/{eduId}")]
        public IActionResult updateEducation(string userId, int eduId, AdminPostEducationDto edu)
        {
            try
            {
                if (edu == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                    return NotFound(_responseObject);
                }

                var eduToUpdate = _mapper.Map<Education>(edu);

                _education.updateEducationRequest(userId, eduId, eduToUpdate);

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull");

                return Ok(_responseObject);
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "An error occurred while updating education details.");
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while updating education details.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
          
        }

        // DELETE api/<AdminApprovalController>/5
        [HttpDelete("education/{eduId}")]
        public IActionResult DeleteEducation(string userId, int eduId)
        {
            try
            {
                if (eduId == 0)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                    return NotFound(_responseObject);
                }

                _education.removeEducationRequest(userId, eduId);

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull");


                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting education details.");
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while deleting education details.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }

        }

        // Skill Approval Endpoints

        [HttpGet("getskills")]
        public ActionResult<List<AdminGetSkillDto>> GetUserSkills(string userId)
        {
            try
            {
                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                    return NotFound(_responseObject);
                }

                var skillsFromDB = _skill.GetSkillsByUserID(userId);


                return Ok(skillsFromDB);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user skills.");
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while retrieving user skills.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }

        }

        [HttpPut("skill/{skillId}")]
        public IActionResult updateSkill(string userId, int skillId, AdminPostSkillDto edu)
        {
            try
            {

                if (edu == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                    return NotFound(_responseObject);
                }

                var skillToUpdate = _mapper.Map<Skills>(edu);

                _skill.updateSkillsRequest(userId, skillId, skillToUpdate);

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull");

                return Ok(_responseObject);
            }
          
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating user skills.");
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while updating user skills.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
        }

        [HttpDelete("skill/{skillId}")]
        public IActionResult DeleteSkill(string userId, int skillId)
        {
            try
            {
                if (skillId == 0)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                    return NotFound(_responseObject);
                }

                _skill.removeSkillsRequest(userId, skillId);

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull");

                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting user skills.");
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while deleting user skills.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }

        }




        // Experience Approval Endpoints


        [HttpGet("getexperience")]
        public ActionResult<List<AdminGetExperienceDto>> GetUserExperiences(string userId)
        {
            try
            {
                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "No Experience found associated with current user");
                    return NotFound(_responseObject);
                }


                var experienceFromDB = _experience.GetUserExperience(userId);

                return Ok(experienceFromDB);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user Experiences.");
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while retrieving user Experiences.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }


        }

        [HttpPut("experience/{expId}")]
        public IActionResult updateExperience(string userId, int expId, AdminPostExperienceDto exp)
        {
            try
            {
                if (exp == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                    return NotFound(_responseObject);
                }

                var ExperienceToUpdate = _mapper.Map<UserExperience>(exp);

                _experience.UpdateUserExperienceRequest(userId, expId, ExperienceToUpdate);

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull");

                return Ok(_responseObject);

            }
           
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating Experiences.");
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while updating Experiences.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }

        }

        [HttpDelete("experience/{expId}")]
        public IActionResult DeleteExperience(string userId, int expId)
        {
            try
            {
                if (expId == 0)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                    return NotFound(_responseObject);
                }

                _experience.RemoveUserExperienceRequest(userId, expId);

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull");

                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting Experiences.");
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while deleting Experiences.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
          
        }




        //Artical Approval Endpoints

        [HttpGet("getarticals")]
        public ActionResult<List<AdminGetExperienceDto>> GetUserArticals(string userId)
        {
            try
            {
                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "No Experience found associated with current user");
                    return NotFound(_responseObject);
                }

                var userBlogsFromDb = _userBlogs.GetByUserId(userId);

                return Ok(userBlogsFromDb);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving articals.");
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while retrieving articals.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }

        }

        [HttpPut("artical/{articalId}")]
        public IActionResult updateArtical(string userId, int articalId, AdminBlogPostDto blogs)
        {
            try
            {
                if (blogs == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                    return NotFound(_responseObject);
                }

                var ExperienceToUpdate = _mapper.Map<UserBlogs>(blogs);

                _userBlogs.updateBlogsRequest(userId, articalId, ExperienceToUpdate);

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull");

                return Ok(_responseObject);
            }
           
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating articals.");
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while updating articals.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
        }

        [HttpDelete("artical/{articalId}")]
        public IActionResult DeleteArtical(string userId, int articalId)
        {
           
            try
            {
                if (articalId == 0)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                    return NotFound(_responseObject);
                }

                _userBlogs.removeBlogsRequest(userId, articalId);

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull");

                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting articals.");
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while deleting articals.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
        }

        // Product APIs

        [HttpGet("getproducts")]
        public ActionResult<List<AdminGetExperienceDto>> GetUserProducts(string userId)
        {
            try
            {
                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "No Product found associated with current user");
                    return NotFound(_responseObject);
                }

                var userProductsFromDb = _product.GetProductsByUserID(userId);
                return Ok(userProductsFromDb);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving articals.");
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while retrieving articals.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
         

        }

        [HttpPut("product/{productId}")]
        public IActionResult UpdateProduct(string userId, int productId, AdminProductPostDto products)
        {
            try
            {
                if (products == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                    return NotFound(_responseObject);
                }

                var ProductsToUpdate = _mapper.Map<UserProducts>(products);

                _product.updateProductsRequest(userId, productId, ProductsToUpdate);

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Product Updated Successfully");

                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating product.");
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while  updating product.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }

        }

        [HttpDelete("product/{productId}")]
        public IActionResult DeleteProduct(string userId, int productId)
        {
            try
            {
                if (productId == 0)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                    return NotFound(_responseObject);
                }

                _product.removeProductsRequest(userId, productId);

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Product Deleted Successfully");

                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting product.");
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while  deleting product.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }

        }

        // Service APIs

        [HttpGet("getservices")]
        public ActionResult<List<AdminGetExperienceDto>> GetUserService(string userId)
        {
            try
            {
                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "No Product found associated with current user");
                    return NotFound(_responseObject);
                }

                var userProductsFromDb = _service.GetServiceGigByUserId(userId);

                return Ok(userProductsFromDb);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user services.");
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while retrieving user services.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }


        }

        [HttpPut("service/{serviceId}")]
        public IActionResult UpdateService(string userId, int serviceId, AdminUserServiceGigPostDto service)
        {
            try
            {
                if (service == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                    return NotFound(_responseObject);
                }

                var ProductsToUpdate = _mapper.Map<UserServiceGig>(service);

                _service.updateServiceGigRequest(userId, serviceId, ProductsToUpdate);

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Product Updated Successfully");

                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating user services.");
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while updating user services.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
           
        }

        [HttpDelete("service/{serviceId}")]
        public IActionResult DeleteService(string userId, int serviceId)
        {
            try
            {

                if (serviceId == 0)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                    return NotFound(_responseObject);
                }

                _service.removeServiceGigRequest(userId, serviceId);

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Product Deleted Successfully");

                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting user services.");
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while deleting user services.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }

        }
    }
}
