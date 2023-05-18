using AutoMapper;
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
        private readonly IMapper _mapper;

        public AdminApprovalController(IEducation education, IUserExperience experience, ISkills skill, IMapper mapper)
        {
            _education = education;
            _experience = experience;
            _skill = skill;
            _mapper = mapper;
        }

        // GET: api/<EducationController>
        [HttpGet("geteducation")]
        public ActionResult<IEnumerable<AdminGetEducation>> GetEducationDetailsAdmin(int userId)
        {
            if (userId == 0)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                return NotFound(_responseObject);
            }

            var educationDetails = _education.GetDetails(userId);

            //var finalEduDetail = _mapper.Map<IEnumerable<AdminGetEducation>>(educationDetails);

            //_responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", educationDetails);

            return Ok(educationDetails);
        }


        // Education Approval Endpoints

        [HttpPut("education/{eduId}")]
        public IActionResult updateEducation(int userId, int eduId, AdminPostEducationDto edu)
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

        // DELETE api/<AdminApprovalController>/5
        [HttpDelete("education/{eduId}")]
        public IActionResult DeleteEducation(int userId, int eduId)
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

        // Skill Approval Endpoints

        [HttpGet("getskills")]
        public ActionResult<List<AdminGetSkillDto>> GetUserSkills(int userId)
        {
            if (userId == 0)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                return NotFound(_responseObject);
            }

            var skillsFromDB = _skill.GetSkillsByUserID(userId);

            //var skillsDto = _mapper.Map<IEnumerable<AdminGetSkillDto>>(skillsFromDB);

            //_responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", skillsFromDB);

            return Ok(skillsFromDB);
        }

        [HttpPut("skill/{skillId}")]
        public IActionResult updateSkill(int userId, int skillId, AdminPostSkillDto edu)
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

            _skill.removeSkillsRequest(userId, skillId);

            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull");

            return Ok(_responseObject);
        }

        // Experience Approval Endpoints

        //Get
        [HttpGet("getexperience")]
        public ActionResult<List<AdminGetExperienceDto>> GetUserExperiences(int userId)
        {
            if (userId == null)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "No Experience found associated with current user");
                return NotFound(_responseObject);
            }


            var experienceFromDB = _experience.GetUserExperience(userId);

            //var experienceDto = _mapper.Map<List<AdminGetExperienceDto>>(experienceFromDB);

            //_responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", experienceFromDB);

            return Ok(experienceFromDB);

        }

        [HttpPut("experience/{expId}")]
        public IActionResult updateExperience(int userId, int expId, AdminPostExperienceDto exp)
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

        [HttpDelete("experience/{expId}")]
        public IActionResult DeleteExperience(int userId, int expId)
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

        //Artical Approval Endpoints

        //[HttpGet("getarticals")]
        //public ActionResult<List<AdminGetExperienceDto>> GetUserArticals(int userId)
        //{
        //    if (userId == 0)
        //    {
        //        _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "No Experience found associated with current user");
        //        return NotFound(_responseObject);
        //    }


        //    var experienceFromDB = _experience.GetUserExperience(userId);

        //    //var experienceDto = _mapper.Map<List<AdminGetExperienceDto>>(experienceFromDB);

        //    //_responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", experienceFromDB);

        //    return Ok(experienceFromDB);

        //}

        //[HttpPut("artical/{articalId}")]
        //public IActionResult updateArtical(int userId, int articalId, AdminPostExperienceDto exp)
        //{
        //    if (exp == null)
        //    {
        //        _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
        //        return NotFound(_responseObject);
        //    }

        //    var ExperienceToUpdate = _mapper.Map<UserExperience>(exp);

        //    _experience.UpdateUserExperienceRequest(userId, expId, ExperienceToUpdate);

        //    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull");

        //    return Ok(_responseObject);
        //}

        //[HttpDelete("artical/{articalId}")]
        //public IActionResult DeleteArtical(int userId, int articalId)
        //{
        //    if (expId == 0)
        //    {
        //        _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
        //        return NotFound(_responseObject);
        //    }

        //    _experience.RemoveUserExperienceRequest(userId, articalId);

        //    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull");

        //    return Ok(_responseObject);
        //}
    }
}
