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

        public CountryController(IMapper mapper,ICountry CountryRepository)
        {
            _mapper = mapper;
            _countryRepository = CountryRepository;
        }



        [HttpGet]
        public ActionResult<List<CountryDto>> GetUserCountries()
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


        [HttpPost]
        public ActionResult AddUserCountry([FromBody] CountryDto country)
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


        [HttpPut("{countryId}")]
        public ActionResult UpdateUserCountry(int countryId, [FromBody] CountryDto userCountry)
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


        [HttpDelete("{countryId}")]
        public IActionResult DeleteUserCountry(int countryId)
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
    }
}
