using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Controllers
{
    [Route("api/aboutDetails")]
    [Authorize]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfile _userProfileRepository;
        private readonly IMapper _mapper;
		private ResponseObject _responseObject;


		public UserProfileController(IUserProfile UserRepository, IMapper mapper)
        {
            _userProfileRepository = UserRepository;
            _mapper = mapper;
        }

        // GET: api/<AboutController>
        [HttpGet]
        public ActionResult<IEnumerable<UserProfileDto>> aboutDetails()
        {
            var id = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;

            if (!_userProfileRepository.checkAbout(id))
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                return NotFound(_responseObject);
			}
            else
            {
				var aboutFromDB = _userProfileRepository.GetAbout(id);

				var AboutDto = _mapper.Map<UserProfileDto>(aboutFromDB);
				_responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", AboutDto);
			}
			return Ok(_responseObject);
        }

        // POST api/<AboutController>
        [HttpPost]
        public ActionResult AddAboutDetails (UserProfileDto userProfileDetails)
        {
            var id = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;

            if (id == null)
            {
				_responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                return NotFound(_responseObject);
			}
            else
            {
				var finalAbout = _mapper.Map<UserProfile>(userProfileDetails);

                var aboutAdded = _userProfileRepository.AddAbout(id, finalAbout);

                if (aboutAdded)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "About Details Added Succesfully");
                }
                else
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "About Details already Exists");
                    return BadRequest(_responseObject);
                }

            }

			return Ok(_responseObject);
        }

        // PUT api/<AboutController>/5
        [HttpPut]
        public ActionResult Put(UserProfileDto about)
        {
            var id = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
            if (id == null)
            {
				_responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
			}
            else
            {
				_userProfileRepository.updateAbout(id, about);
				_responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "About Details updated successfully");
			}
			//var finalAbout = _mapper.Map<About>(about);

			return Ok(_responseObject);
        }

        // DELETE api/<AboutController>/5
        [HttpDelete("{aboutId}")]
        public ActionResult Delete(int aboutId)
        {
            var id = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;

            if (id == null)
            {
				_responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
			}
            else
            {
				_userProfileRepository.removeAbout(id, aboutId);
				_responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "About Details successfully");

			}
			return Ok(_responseObject);
        }

        [HttpGet("/api/users")]
        public ActionResult<IEnumerable<UserProfile>> GetAllUsers()
        {
            var users = _userProfileRepository.getUsers();
            return Ok(users);
        }

        [HttpGet("/api/users/pending")]
        public ActionResult<IEnumerable<UserProfile>> GetUsersPendingRequest()
        {
            var id = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;

            var users = _userProfileRepository.getUserPendingRequests(id);
            return Ok(users);
        }
    }
}
