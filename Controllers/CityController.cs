using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Controllers
{
    [Route("api/cities")]
    [Authorize]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICity _cityRepository;
        private readonly IMapper _mapper;
        private ResponseObject _responseObject;
        private readonly ILogger<CityController> _logger;


        public CityController( IMapper mapper, ICity CityRepository, ILogger<CityController> logger)
        {
            _mapper = mapper;
            _cityRepository = CityRepository;
            _logger = logger;

        }
        [HttpGet]
        public ActionResult<List<CityDto>> GetUserCities()
        {
           try
            {
                var citiesFromDB = _cityRepository.GetCities();

                if (citiesFromDB == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result is Empty");
                    return NotFound(_responseObject);
                }

                var CityDto = _mapper.Map<IEnumerable<CityDto>>(citiesFromDB);

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", CityDto);


                return Ok(_responseObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving cities.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while retrieving cities.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);

            }
         

        }


        [HttpPost]
        public ActionResult AddUserCity([FromBody] CityDto city)
        {
            try
            {
                if (city == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Can not add Null Value");
                    return NotFound(_responseObject);
                }

                var cityAdd = _mapper.Map<UserCity>(city);

                _cityRepository.AddCity(cityAdd);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "City Added Succesfully");

                return Ok(_responseObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding cities.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while adding cities.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);

            }
          

        }


        [HttpPut("{cityId}")]
        public ActionResult UpdateUserCity(int cityId, [FromBody] CityDto userCity)
        {
            try
            {
                if (cityId == 0)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                    return NotFound(_responseObject);
                }

                var updateCity = _mapper.Map<UserCity>(userCity);

                _cityRepository.updateCity(cityId, updateCity);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "City Updated succesfully");

                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating cities.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while updating cities.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);

            }

     

        }


        [HttpDelete("{cityId}")]
        public IActionResult DeleteUserSkill(int cityId)
        {
            try
            {
                _cityRepository.removeCity(cityId);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "City Deleted successfully");

                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting cities.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while deleting cities.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);

            }

        }
    }
}
