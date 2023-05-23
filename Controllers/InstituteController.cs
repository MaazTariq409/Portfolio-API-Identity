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
    [Route("api/institutes")]
    [Authorize]
    [ApiController]
    public class InstituteController : ControllerBase
    {
        private readonly IInstitute _instituteRepository;
        private readonly IMapper _mapper;
        private ResponseObject _responseObject;

        public InstituteController(IMapper mapper, IInstitute InstituteRepository)
        {
            _mapper = mapper;
            _instituteRepository = InstituteRepository;
        }

        [HttpGet]
        public ActionResult<List<InstituteDto>> GetUserInstitute()
        {

            var instituteFromDB = _instituteRepository.GetInstitutes();

            if (instituteFromDB == null)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result is Empty");
                return NotFound(_responseObject);
            }

            var InstituteDto = _mapper.Map<IEnumerable<InstituteDto>>(instituteFromDB);

            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", InstituteDto);


            return Ok(_responseObject);

        }


        [HttpPost]
        public ActionResult AddUserInstitute([FromBody] InstituteDto institute)
        {
            if (institute == null)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Can not add Null Value");
                return NotFound(_responseObject);
            }

            var instituteAdd = _mapper.Map<UserInstitute>(institute);

            _instituteRepository.AddInstitute(instituteAdd);
            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Institute Added Succesfully");

            return Ok(_responseObject);

        }


        [HttpPut("{instituteId}")]
        public ActionResult UpdateUserInstitute(int instituteId, [FromBody] InstituteDto userInstitute)
        {

            if (instituteId == 0)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                return NotFound(_responseObject);
            }

            var updateCountry = _mapper.Map<UserInstitute>(userInstitute);

            _instituteRepository.updateInstitute(instituteId, updateCountry);
            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Institute Updated succesfully");

            return Ok(_responseObject);

        }


        [HttpDelete("{instituteId}")]
        public IActionResult DeleteUserInstitute(int instituteId)
        {

            if (instituteId == 0)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                return NotFound(_responseObject);
            }

            _instituteRepository.removeInstitute(instituteId);
            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Institute Deleted successfully");

            return Ok(_responseObject);

        }
    }
}
