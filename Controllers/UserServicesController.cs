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

        public UserServicesController( IMapper mapper, IUserServices ServicesRepository)
        {
            _mapper = mapper;
            _servicesRepository = ServicesRepository;
        }

        [HttpGet]
        public ActionResult<List<AdminUserServiceGetDto>> GetServices()
        {
           
            var servicesFromDb = _servicesRepository.GetServices();

            if(servicesFromDb == null)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "No Services Found");
                return NotFound(_responseObject);
            }

            var ServiceDto = _mapper.Map<IEnumerable<AdminUserServiceGetDto>>(servicesFromDb);

            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", ServiceDto);


            return Ok(_responseObject);

        }


        [HttpPost]
        public ActionResult AddService([FromBody] AdminUserServicePostDto service)
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


        [HttpPut("{serviceId}")]
        public ActionResult UpdateUserService(int serviceId, [FromBody] AdminUserServicePostDto userService)
        {
        
            var updateService = _mapper.Map<UserServices>(userService);

            _servicesRepository.updateServices(serviceId, updateService);
            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Service Updated succesfully");

            return Ok(_responseObject);

        }


        [HttpDelete("{serviceId}")]
        public IActionResult DeleteUserService(int serviceId)
        {
            _servicesRepository.removeServices(serviceId);
            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Service Deleted successfully");

            return Ok(_responseObject);

        }
    }
}
