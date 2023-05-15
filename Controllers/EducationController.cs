using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
using Portfolio_API.Repository;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Controllers
{
    [Route("api/education")]
    [Authorize]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly IEducation _EducationRepository;
        private readonly IMapper _mapper;
		private ResponseObject _responseObject;


		public EducationController (IEducation education, IMapper mapper)
        {
            _EducationRepository = education;
            _mapper = mapper;
        }

        // GET: api/<EducationController>
        [HttpGet]
        public ActionResult<IEnumerable<EducationDto>> GetEducationDetails()
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);
            if (userId == 0)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                return NotFound(_responseObject);
            }

            var educationDetails = _EducationRepository.GetDetails(userId);
            var finalEduDetail = _mapper.Map<IEnumerable<EducationDto>>(educationDetails);
            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", finalEduDetail);

            return Ok(_responseObject);
        }

        // POST api/<EducationController>
        [HttpPost]
        public ActionResult AddEduDetails(IEnumerable<EducationDto> Edu)
        {
            var id = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);

            if (id == 0)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                return NotFound(_responseObject);
            }

            var finalEdu = _mapper.Map<IEnumerable<Education>>(Edu);
            _EducationRepository.AddEducation(id, finalEdu);
            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Education Details added succesfully");

            return Ok(_responseObject);
        }

        //// PUT api/<EducationController>/5
        [HttpPut]
        public ActionResult UpdateEdu( int eduId, EducationDto Edu)
        {
            var id = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);

            if (id == 0 || eduId == 0)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                return NotFound(_responseObject);
            }

            var finalEdu = _mapper.Map<Education>(Edu);
            _EducationRepository.updateEducation(id, eduId, finalEdu);
            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Education Details Updated succesfully");

            return Ok(_responseObject);
        }

        // DELETE api/<EducationController>/5
        [HttpDelete]
        public ActionResult DeleteEdu (int eduId)
        {
            var id = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);

            if (id == 0 || eduId == 0)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                return NotFound(_responseObject);
            }
            _EducationRepository.removeEducation(id, eduId);
            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Education Details Deleted succesfully");

            return Ok(_responseObject);
        }
    }
}
