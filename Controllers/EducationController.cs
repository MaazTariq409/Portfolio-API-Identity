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
        private readonly ILogger<EducationController> _logger;



        public EducationController(IEducation education, IMapper mapper, ILogger<EducationController> logger)
        {
            _EducationRepository = education;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/<EducationController>
        [HttpGet]
        public ActionResult<IEnumerable<EducationDto>> GetEducationDetails()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                    return NotFound(_responseObject);
                }

                var educationDetails = _EducationRepository.GetDetails(userId);

                var ApprovedEduDetails = educationDetails.Where(x => x.status != "pending").ToList();

                var finalEduDetail = _mapper.Map<IEnumerable<EducationDto>>(ApprovedEduDetails);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", finalEduDetail);

                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user education.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while retrieving user education.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);

            }
            
        }

        // POST api/<EducationController>
        [HttpPost]
        public ActionResult AddEduDetails(IEnumerable<EducationDto> Edu)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;

                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                    return NotFound(_responseObject);
                }

                var finalEdu = _mapper.Map<IEnumerable<Education>>(Edu);
                _EducationRepository.AddEducation(userId, finalEdu);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Education Details added succesfully");

                return Ok(_responseObject);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "An error occurred while adding user education.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while adding user education.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
           
        }

        //// PUT api/<EducationController>/5
        [HttpPut("{eduId}")]
        public ActionResult UpdateEdu(int eduId, [FromBody] EducationDto Edu)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;

                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                    return NotFound(_responseObject);
                }

                var finalEdu = _mapper.Map<Education>(Edu);
                _EducationRepository.updateEducation(userId, eduId, finalEdu);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Education Details Updated succesfully");

                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating user education.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while updating user education.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
           
        }

        // DELETE api/<EducationController>/5
        [HttpDelete("{eduId}")]
        public ActionResult DeleteEdu (int eduId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;

                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                    return NotFound(_responseObject);
                }
                _EducationRepository.removeEducation(userId, eduId);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Education Details Deleted succesfully");

                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting user education.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while deleting user education.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);

            }
          
        }
    }
}
