using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio_API.DTOs;
using Portfolio_API.DTOs.Admin;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Controllers
{
    [Route("api/services")]
    [ApiController]
    public class UserServicesController : ControllerBase
    {
        private readonly IUserServices _servicesRepository;
        private readonly IMapper _mapper;
        private ResponseObject _responseObject;
        private readonly ILogger<UserServicesController> _logger;

        public UserServicesController( IMapper mapper, IUserServices ServicesRepository, ILogger<UserServicesController> logger)
        {
            _mapper = mapper;
            _servicesRepository = ServicesRepository;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<AdminUserServiceGetDto>> GetServices()
        {
            try
            {
                var servicesFromDb = _servicesRepository.GetServices();

                if (servicesFromDb == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "No Services Found");
                    return NotFound(_responseObject);
                }

                var ServiceDto = _mapper.Map<IEnumerable<AdminUserServiceGetDto>>(servicesFromDb);

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", ServiceDto);


                return Ok(_responseObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving services.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while retrieving services.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }

           

        }


        [HttpPost]
        public ActionResult AddService([FromBody] AdminUserServicePostDto service)
        {
            try
            {
                if (service == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Can not add Null Value");
                    return NotFound(_responseObject);
                }

                var serviceAdd = _mapper.Map<UserServices>(service);

                _servicesRepository.AddServices(serviceAdd);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Service Added Succesfully");

                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding service.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while adding service.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
           

        }


        [HttpPut("{serviceId}")]
        public ActionResult UpdateUserService(int serviceId, [FromBody] AdminUserServicePostDto userService)
        {
            try
            {
                var updateService = _mapper.Map<UserServices>(userService);

                _servicesRepository.updateServices(serviceId, updateService);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Service Updated succesfully");

                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating service.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while updating service.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
           

        }


        [HttpDelete("{serviceId}")]
        public IActionResult DeleteUserService(int serviceId)
        {
            try
            {
                _servicesRepository.removeServices(serviceId);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Service Deleted successfully");

                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deletin service.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while deleting service.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
          

        }
    }
}
