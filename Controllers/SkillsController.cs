using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Portfolio_API.Data;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
using Portfolio_API.Repository;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Controllers
{
    [Route("api/skills")]
    [Authorize]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkills _skillsRepository;
        private readonly IMapper _mapper;
        private ResponseObject _responseObject;
        private readonly ILogger<SkillsController> _logger;

        public SkillsController( IMapper mapper, ISkills SkillsRepository, ILogger<SkillsController> logger)
        {
            _mapper = mapper;
            _skillsRepository = SkillsRepository;
            _logger = logger;


        }
        [HttpGet]
        public ActionResult<List<SkillsDto>> GetUserSkills()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;

                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                    return NotFound(_responseObject);
                }

                var skillsFromDB = _skillsRepository.GetSkillsByUserID(userId);

                var approvedSkills = skillsFromDB.Where(x => x.status != "pending").ToList();

                var skillsDto = _mapper.Map<IEnumerable<SkillsDto>>(approvedSkills);

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Successful", skillsDto);

                return Ok(_responseObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user skills.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while retrieving user skills.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }

        }


        //POST api/<SkillsController>
        [HttpPost]
        public ActionResult AddUserSkill([FromBody] IEnumerable<SkillsDto> userSkill)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                    return NotFound(_responseObject);
                }

                var AddSkill = _mapper.Map<IEnumerable<Skills>>(userSkill);

                _skillsRepository.AddSkillsByUserID(userId, AddSkill);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Skilled Added Succesfully");

                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding user skills.");
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while adding user skills.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }


        }


        [HttpPut("{skillId}")]
        public ActionResult UpdateUserSkill(int skillId, SkillsDto userSkill)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                    return NotFound(_responseObject);
                }

                var updateskills = _mapper.Map<Skills>(userSkill);

                _skillsRepository.updateSkillsByUserID(userId, skillId, updateskills);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Skills Updated succesfully");

                return Ok(_responseObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating user skills.");
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while updating user skills.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);

            }
            

        }

        [HttpDelete("{skillId}")]
        public IActionResult DeleteUserSkill(int skillId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;

                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                    return NotFound(_responseObject);
                }

                _skillsRepository.removeSkillsByUserID(userId, skillId);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Skills Deleted successfully");

                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting user skills.");
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while deleting user skills.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
          

        }
    }
}
