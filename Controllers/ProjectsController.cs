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
    [Route("api/projects")]
    [Authorize]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjects _projectRepository;
        private readonly IMapper _mapper;
        private ResponseObject _responseObject;

        public ProjectsController( IMapper mapper, IProjects ProjectsRepository)
        {
            _mapper = mapper;
            _projectRepository = ProjectsRepository;

        }
        [HttpGet]
        public ActionResult<List<UserProjectsDto>> GetUserProjects()
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);

            if(userId == null)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not Found");
                return NotFound(_responseObject);
            }

            var projectsFromDB = _projectRepository.GetProjectsByUserID(userId);
            var projectsDto = _mapper.Map<IEnumerable<UserProjectsDto>>(projectsFromDB);
            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Projects Added Successfully", projectsDto);
            return Ok(projectsDto);
            

        }


        [HttpPost]
        public ActionResult AddUserProjects([FromBody] IEnumerable<UserProjectsDto> userProject)
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);

            if(userId == 0)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not Found");
                return NotFound(_responseObject);
            }

            var addProjects = _mapper.Map<IEnumerable<UserProjects>>(userProject);

            _projectRepository.AddProjectsByUserID(userId, addProjects);
            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Projects Added Successfully");

            return Ok(_responseObject);

        }

        //[AllowAnonymous]
        [HttpPut]
        public ActionResult UpdateUserProjects(int projectId, [FromBody] UserProjectsDto userProject)
        {

            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);

            if (userId == 0)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not Found");
                return NotFound(_responseObject);
            }

            var updateProject = _mapper.Map<UserProjects>(userProject);

            _projectRepository.updateProjectsByUserID(userId, projectId, updateProject);
            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Project Updated Successfully");

            return Ok(_responseObject);

        }


        [HttpDelete]
        public IActionResult DeleteUserProject(int projectId)
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);

            if (userId == 0 || projectId == 0)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "Result not found");
                return NotFound(_responseObject);
            }

            _projectRepository.removeProjectsByUserID(userId, projectId);
            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Project Deleted successfully");

            return Ok(_responseObject);

        }
    }
}
