using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Controllers
{
    [Route("api/servicegig")]
    [Authorize]
    [ApiController]
    public class UserServiceGigController : ControllerBase
    {
        private readonly IUserServiceGig _serviceGigRepository;
        private readonly IMapper _mapper;
        private ResponseObject _responseObject;
        private readonly ILogger<UserServiceGigController> _logger;

        public UserServiceGigController( IMapper mapper, IUserServiceGig serviceGigRepository, ILogger<UserServiceGigController> logger)
        {
            _mapper = mapper;
            _serviceGigRepository = serviceGigRepository;
            _logger = logger;
        }


        //Get
        [AllowAnonymous]
        [HttpGet("/api/servicegigs")]

        public ActionResult<List<UserServiceGigDto>> GetAllServiceGig()
        {
            try
            {
                var serviceGigFromDb = _serviceGigRepository.GetAllGig();
                if (serviceGigFromDb == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "No User Service Gig found in database");
                    return NotFound(_responseObject);
                }

                var approvedserviceGig = serviceGigFromDb.Where(x => x.Status != "pending").ToList();

                UserServiceGigDto[] serviceGigDto = new UserServiceGigDto[approvedserviceGig.Count];

                for (int i = 0; i < approvedserviceGig.Count; i++)
                {
                    serviceGigDto[i] = new UserServiceGigDto();
                    serviceGigDto[i].Title = approvedserviceGig[i].Title;
                    serviceGigDto[i].Image = approvedserviceGig[i].Image;
                    serviceGigDto[i].Price = approvedserviceGig[i].Price;
                    serviceGigDto[i].DateCreated = approvedserviceGig[i].DateCreated;
                    serviceGigDto[i].Description = approvedserviceGig[i].Description;

                    serviceGigDto[i].UserProfileImage = approvedserviceGig[i].UserProfile.ProfileUrl;
                    serviceGigDto[i].UserLanguage = approvedserviceGig[i].UserProfile.Language;
                    serviceGigDto[i].UserName = approvedserviceGig[i].UserProfile.Name;
                    serviceGigDto[i].UserIntroduction = approvedserviceGig[i].UserProfile.Introduction;
                }

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", serviceGigDto);
                return Ok(_responseObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving  service gigs.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while retrieving  service gigs.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
           
        }


        [AllowAnonymous]
        [HttpGet("/api/servicegigbyid/{serviceId}")]
        public ActionResult<List<UserServiceGigDto>> GetAllGigByServiceId(int serviceId)
        {
            try
            {
                if (serviceId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Null input error");
                    return NotFound(_responseObject);
                }

                var serviceGigFromDb = _serviceGigRepository.GetAllGigByServiceId(serviceId);
                if (serviceGigFromDb == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "No User Service Gig found in database");
                    return NotFound(_responseObject);
                }

                var approvedserviceGig = serviceGigFromDb.Where(x => x.Status != "pending").ToList();

                UserServiceGigDto[] serviceGigDto = new UserServiceGigDto[approvedserviceGig.Count];

                for (int i = 0; i < approvedserviceGig.Count; i++)
                {
                    serviceGigDto[i] = new UserServiceGigDto();
                    serviceGigDto[i].Title = approvedserviceGig[i].Title;
                    serviceGigDto[i].Image = approvedserviceGig[i].Image;
                    serviceGigDto[i].Price = approvedserviceGig[i].Price;
                    serviceGigDto[i].DateCreated = approvedserviceGig[i].DateCreated;
                    serviceGigDto[i].Description = approvedserviceGig[i].Description;

                    serviceGigDto[i].UserProfileImage = approvedserviceGig[i].UserProfile.ProfileUrl;
                    serviceGigDto[i].UserLanguage = approvedserviceGig[i].UserProfile.Language;
                    serviceGigDto[i].UserName = approvedserviceGig[i].UserProfile.Name;
                    serviceGigDto[i].UserIntroduction = approvedserviceGig[i].UserProfile.Introduction;
                }

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", serviceGigDto);
                return Ok(_responseObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving service gigs by id.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while retrieving  service gigs by id.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
            
        }


        [HttpGet]
        public ActionResult<List<UserServiceGigDto>> GetUserServiceGig()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "User not found in database");
                    return NotFound(_responseObject);
                }

                var serviceGigFromDb = _serviceGigRepository.GetServiceGigByUserId(userId);
                if (serviceGigFromDb == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "No User Service Gig found in database against User");
                    return NotFound(_responseObject);
                }

                var approvedserviceGig = serviceGigFromDb.Where(x => x.Status != "pending").ToList();

                UserServiceGigDto[] serviceGigDto = new UserServiceGigDto[approvedserviceGig.Count];

                for (int i = 0; i < approvedserviceGig.Count; i++)
                {
                    serviceGigDto[i] = new UserServiceGigDto();
                    serviceGigDto[i].Title = approvedserviceGig[i].Title;
                    serviceGigDto[i].Image = approvedserviceGig[i].Image;
                    serviceGigDto[i].Price = approvedserviceGig[i].Price;
                    serviceGigDto[i].DateCreated = approvedserviceGig[i].DateCreated;
                    serviceGigDto[i].Description = approvedserviceGig[i].Description;

                    serviceGigDto[i].UserProfileImage = approvedserviceGig[i].UserProfile.ProfileUrl;
                    serviceGigDto[i].UserLanguage = approvedserviceGig[i].UserProfile.Language;
                    serviceGigDto[i].UserName = approvedserviceGig[i].UserProfile.Name;
                    serviceGigDto[i].UserIntroduction = approvedserviceGig[i].UserProfile.Introduction;
                }

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", serviceGigDto);
                return Ok(_responseObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user service gigs.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while retrieving user service gigs.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
           
        }


        //POST 
        [HttpPost("{serviceGigId}")]
        public ActionResult AddUserServiceGig(int serviceGigId , [FromBody] UserServiceGigPostDto userServiceGig)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "User not found in database");
                    return NotFound(_responseObject);
                }

                var addServiceGig = _mapper.Map<UserServiceGig>(userServiceGig);

                _serviceGigRepository.AddServiceGig(userId, serviceGigId, addServiceGig);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "User Service Added Succesfully");

                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding user service gigs.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while adding user service gigs.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
            

        }


        [HttpPut("{serviceGigId}")]
        public ActionResult UpdateUserServiceGig(int serviceGigId, [FromBody] UserServiceGigPostDto userServiceGig)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "User id not found");
                    return NotFound(_responseObject);
                }

                var updateServiceGig = _mapper.Map<UserServiceGig>(userServiceGig);
                _serviceGigRepository.updateServiceGig(userId, serviceGigId, updateServiceGig);

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "User Service Updated succesfully");

                return Ok(_responseObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating user service gigs.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while updating user service gigs.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
          

        }


        [HttpDelete("{ServiceGigId}")]
        public IActionResult DeleteUserServiceGig(int ServiceGigId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;

                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "User not found");
                    return NotFound(_responseObject);
                }
                _serviceGigRepository.removeServiceGig(userId, ServiceGigId);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "User Service Deleted successfully");

                return Ok(_responseObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting user service gigs.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while deleting user service gigs.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
          
        }
    }
}
