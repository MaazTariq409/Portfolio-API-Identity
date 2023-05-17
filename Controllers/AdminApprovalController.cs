using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
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
        private readonly IMapper _mapper;

        public AdminApprovalController(IEducation education, IUserExperience experience, ISkills skill, IMapper mapper)
        {
            _education = education;
            _experience = experience;
            _skill = skill;
            _mapper = mapper;
        }

        // Education Approval Endpoints

        [HttpPut("education/{eduId}")]
        public IActionResult updateEducation(int userId, int eduId, AdminEducationDto edu)
        {
            if (edu == null)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                return NotFound(_responseObject);
            }

            var eduToUpdate = _mapper.Map<Education>(edu);

            _education.updateEducation(userId, eduId, eduToUpdate);

            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull");

            return Ok(_responseObject);
        }

        // DELETE api/<AdminApprovalController>/5
        [HttpDelete("education/{eduId}")]
        public IActionResult DeleteEducation(int userId, int eduId)
        {
            if (eduId == 0)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                return NotFound(_responseObject);
            }

            _education.removeEducation(userId, eduId);

            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull");


            return Ok(_responseObject);
        }

        // Skill Approval Endpoints

        [HttpPut("skill/{skillId}")]
        public IActionResult updateSkill(int userId, int skillId, adminSkillDto edu)
        {
            if (edu == null)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                return NotFound(_responseObject);
            }

            var skillToUpdate = _mapper.Map<Skills>(edu);

            _skill.updateSkillsByUserID(userId, skillId, skillToUpdate);

            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull");

            return Ok(_responseObject);
        }

        [HttpDelete("skill/{skillId}")]
        public IActionResult DeleteSkill(int userId, int skillId)
        {
            if (skillId == 0)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                return NotFound(_responseObject);
            }

            _skill.removeSkillsByUserID(userId, skillId);

            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull");

            return Ok(_responseObject);
        }

        // Experience Approval Endpoints

        [HttpPut("experience/{expId}")]
        public IActionResult updateExperience(int userId, int expId, AdminExperienceDto exp)
        {
            if (exp == null)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                return NotFound(_responseObject);
            }

            var ExperienceToUpdate = _mapper.Map<UserExperience>(exp);

            _experience.UpdateUserExperience(userId, expId, ExperienceToUpdate);

            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull");

            return Ok(_responseObject);
        }

        [HttpDelete("experience/{expId}")]
        public IActionResult DeleteExperience(int userId, int expId)
        {
            if (expId == 0)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                return NotFound(_responseObject);
            }

            _experience.RemoveUserExperience(userId, expId);

            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull");

            return Ok(_responseObject);
        }
    }
}
