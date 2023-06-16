using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Controllers
{
    [Route("api/countries")]
    [Authorize]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountry _countryRepository;
        private readonly IMapper _mapper;
        private ResponseObject _responseObject;
        private readonly ILogger<CountryController> _logger;


        public CountryController(IMapper mapper,ICountry CountryRepository, ILogger<CountryController> logger)
        {
            _mapper = mapper;
            _countryRepository = CountryRepository;
            _logger = logger;
        }



        [HttpGet]
        public ActionResult<List<CountryDto>> GetUserCountries()
        {
            try
            {
                var countriesFromDB = _countryRepository.GetCountries();

                if (countriesFromDB == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result is Empty");
                    return NotFound(_responseObject);
                }

                var CountryDto = _mapper.Map<IEnumerable<CountryDto>>(countriesFromDB);

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", CountryDto);


                return Ok(_responseObject);

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving countries.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while retrieving countries.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }

           

        }


        [HttpPost]
        public ActionResult AddUserCountry([FromBody] CountryDto country)
        {
            try
            {
                if (country == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Can not add Null Value");
                    return NotFound(_responseObject);
                }

                var countryAdd = _mapper.Map<UserCountry>(country);

                _countryRepository.AddCountry(countryAdd);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Country Added Succesfully");

                return Ok(_responseObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding country.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while adding country.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }          
        }


        [HttpPut("{countryId}")]
        public ActionResult UpdateUserCountry(int countryId, [FromBody] CountryDto userCountry)
        {
            try
            {
                if (countryId == 0)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                    return NotFound(_responseObject);
                }

                var updateCountry = _mapper.Map<UserCountry>(userCountry);

                _countryRepository.updateCountry(countryId, updateCountry);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Country Updated succesfully");

                return Ok(_responseObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating country.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while updating country.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }


           

        }


        [HttpDelete("{countryId}")]
        public IActionResult DeleteUserCountry(int countryId)
        {
            try
            {
                if (countryId == 0)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                    return NotFound(_responseObject);
                }

                _countryRepository.removeCountry(countryId);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Country Deleted successfully");

                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting country.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while deleting country.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
        }
    }
}
