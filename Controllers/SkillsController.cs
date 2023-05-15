using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public SkillsController( IMapper mapper, ISkills SkillsRepository)
        {
            _mapper = mapper;
            _skillsRepository = SkillsRepository;

        }
        [HttpGet]
        public ActionResult<List<SkillsDto>> GetUserSkills()
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);

            if (userId == null)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                return NotFound(_responseObject);
            }


            var skillsFromDB = _skillsRepository.GetSkillsByUserID(userId);

            var skillsDto = _mapper.Map<IEnumerable<SkillsDto>>(skillsFromDB);

            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", skillsDto);


            return Ok(_responseObject);

        }


        //POST api/<SkillsController>
        [HttpPost]
        public ActionResult AddUserSkill([FromBody] IEnumerable<SkillsDto> userSkill)
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);
            if (userId == 0)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                return NotFound(_responseObject);
            }

            var AddSkill = _mapper.Map<IEnumerable<Skills>>(userSkill);

            _skillsRepository.AddSkillsByUserID(userId, AddSkill);
            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Skilled Added Succesfully");

            return Ok(_responseObject);

        }


        [HttpPut("{skillId}")]
        public ActionResult UpdateUserSkill(int skillId, [FromBody] SkillsDto userSkill)
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);
            if (userId == 0 || skillId == 0)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                return NotFound(_responseObject);
            }

            var updateskills = _mapper.Map<Skills>(userSkill);

            _skillsRepository.updateSkillsByUserID(userId, skillId, updateskills);
            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Skills Updated succesfully");

            return Ok(_responseObject);

        }


        [HttpDelete]
        public IActionResult DeleteUserSkill(int skillId)
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);

            if (userId == 0 || skillId == 0)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                return NotFound(_responseObject);
            }

            _skillsRepository.removeSkillsByUserID(userId, skillId);
            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Skills Deleted successfully");

            return Ok(_responseObject);

        }
    }
}
