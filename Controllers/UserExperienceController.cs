using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Controllers
{
    [Route("api/userexperience")]
    [Authorize]
    [ApiController]
    public class UserExperienceController : ControllerBase
    {
        private readonly IUserExperience _userExperienceRepository;
        private readonly IMapper _mapper;
        private ResponseObject _responseObject;
        private readonly ILogger<UserExperienceController> _logger;


        public UserExperienceController( IMapper mapper, IUserExperience userExperienceRepository, ILogger<UserExperienceController> logger)
        {
            _mapper = mapper;
            _userExperienceRepository = userExperienceRepository;
            _logger = logger;
        }


        //Get
        [HttpGet]
        public ActionResult<List<UserExperienceDto>> GetUserExperiences()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;

                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "No Experience found associated with current user");
                    return NotFound(_responseObject);
                }


                var experienceFromDB = _userExperienceRepository.GetUserExperience(userId);

                var approvedExp = experienceFromDB.Where(x => x.status != "pending").ToList();

                var experienceDto = _mapper.Map<IEnumerable<UserExperienceDto>>(approvedExp);

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", experienceDto);


                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user blogs.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while retrieving user blogs.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
           

        }


        //POST 
        [HttpPost]
        public ActionResult AddUserExperience([FromBody] IEnumerable<UserExperienceDto> userExperiences)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "User not found in database");
                    return NotFound(_responseObject);
                }

                var addExperience = _mapper.Map<IEnumerable<UserExperience>>(userExperiences);

                _userExperienceRepository.AddUserExperience(userId, addExperience);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "User Experience Added Succesfully");

                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding user experience.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while adding user experience.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
            

        }


        [HttpPut("{experienceid}")]
        public ActionResult UpdateUserExperience(int experienceId, [FromBody] UserExperienceDto userExperience)
            {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "User or Exprience id not found");
                    return NotFound(_responseObject);
                }

                var updateExperience = _mapper.Map<UserExperience>(userExperience);

                _userExperienceRepository.UpdateUserExperience(userId, experienceId, updateExperience);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Experience Updated succesfully");

                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating user experience.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while updating user experience.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
        

        }


        [HttpDelete("{experienceId}")]
        public IActionResult DeleteUserExperience(int experienceId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;

                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "User not found");
                    return NotFound(_responseObject);
                }

                _userExperienceRepository.RemoveUserExperience(userId, experienceId);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Experience Deleted successfully");

                return Ok(_responseObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting user experience.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while deleting user experience.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
           

        }
    }
}
