using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Controllers
{
    [Route("api/userblog")]
    [Authorize]
    [ApiController]
    public class UserBlogsController : ControllerBase
    {
        private readonly IUserBlogs _userBlogsRepository;
        private readonly IMapper _mapper;
        private ResponseObject _responseObject;
        private readonly ILogger<UserBlogsController> _logger;


        public UserBlogsController( IMapper mapper, IUserBlogs userBlogsRepository, ILogger<UserBlogsController> logger)
        {
            _mapper = mapper;
            _userBlogsRepository = userBlogsRepository;
            _logger = logger;
        }


        //Get
        [AllowAnonymous]
        [HttpGet("/api/userblogs")]

        public ActionResult<List<UserBlogsDto>> GetAllBlogs()
        {
            try
            {
                var blogsFromDb = _userBlogsRepository.GetAll();
                if (blogsFromDb == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "No Blogs found in database");
                    return NotFound(_responseObject);
                }

                var approvedBlogs = blogsFromDb.Where(x => x.status != "pending").ToList();

                UserBlogsDto[] userBlogsDto = new UserBlogsDto[approvedBlogs.Count];

                for (int i = 0; i < approvedBlogs.Count; i++)
                {
                    userBlogsDto[i] = new UserBlogsDto();
                    userBlogsDto[i].dateCreated = approvedBlogs[i].dateCreated;
                    userBlogsDto[i].title = approvedBlogs[i].title;
                    userBlogsDto[i].content = approvedBlogs[i].content;
                    userBlogsDto[i].tags = approvedBlogs[i].tags;
                    userBlogsDto[i].imageUrl = approvedBlogs[i].imageUrl;

                    userBlogsDto[i].ProfileUrl = approvedBlogs[i].UserProfile.ProfileUrl;
                    userBlogsDto[i].linkedin = approvedBlogs[i].UserProfile.Linkedin;
                    userBlogsDto[i].Github = approvedBlogs[i].UserProfile.Github;
                    userBlogsDto[i].Name = approvedBlogs[i].UserProfile.Name;
                    userBlogsDto[i].Email = approvedBlogs[i].UserProfile.Email;
                }

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", userBlogsDto);
                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving blogs.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while retrieving blogs.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
           
        }



        [AllowAnonymous]
        [HttpGet("/api/userblogSearched/{tag}")]

        public ActionResult<List<UserBlogsDto>> GetBlogwithKeyword(string tag)
        {
            try
            {
                var blogsFromDb = _userBlogsRepository.GetAboutWithkeyword(tag);
                if (blogsFromDb == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "No Blogs found in database");
                    return NotFound(_responseObject);
                }

                var approvedBlogs = blogsFromDb.Where(x => x.status != "pending").ToList();

                UserBlogsDto[] userBlogsDto = new UserBlogsDto[approvedBlogs.Count];

                for (int i = 0; i < approvedBlogs.Count; i++)
                {
                    userBlogsDto[i] = new UserBlogsDto();
                    userBlogsDto[i].dateCreated = approvedBlogs[i].dateCreated;
                    userBlogsDto[i].title = approvedBlogs[i].title;
                    userBlogsDto[i].content = approvedBlogs[i].content;
                    userBlogsDto[i].tags = approvedBlogs[i].tags;
                    userBlogsDto[i].imageUrl = approvedBlogs[i].imageUrl;

                    userBlogsDto[i].ProfileUrl = approvedBlogs[i].UserProfile.ProfileUrl;
                    userBlogsDto[i].linkedin = approvedBlogs[i].UserProfile.Linkedin;
                    userBlogsDto[i].Github = approvedBlogs[i].UserProfile.Github;
                    userBlogsDto[i].Name = approvedBlogs[i].UserProfile.Name;
                    userBlogsDto[i].Email = approvedBlogs[i].UserProfile.Email;
                }

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", userBlogsDto);
                return Ok(_responseObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving specific blogs.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while retrieving specific blogs.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
          
        }

        [HttpGet]
        public ActionResult<List<UserBlogsDto>> GetUserBlogs()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "User not found in database");
                    return NotFound(_responseObject);
                }

                var blogsFromDb = _userBlogsRepository.GetByUserId(userId);
                if (blogsFromDb == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "No Blogs found in database against User");
                    return NotFound(_responseObject);
                }

                var approvedBlogs = blogsFromDb.Where(x => x.status != "pending").ToList();

                UserBlogsDto[] userBlogsDto = new UserBlogsDto[approvedBlogs.Count];

                for (int i = 0; i < approvedBlogs.Count; i++)
                {
                    userBlogsDto[i] = new UserBlogsDto();
                    userBlogsDto[i].dateCreated = approvedBlogs[i].dateCreated;
                    userBlogsDto[i].title = approvedBlogs[i].title;
                    userBlogsDto[i].content = approvedBlogs[i].content;
                    userBlogsDto[i].tags = approvedBlogs[i].tags;
                    userBlogsDto[i].imageUrl = approvedBlogs[i].imageUrl;

                    userBlogsDto[i].ProfileUrl = approvedBlogs[i].UserProfile.ProfileUrl;
                    userBlogsDto[i].linkedin = approvedBlogs[i].UserProfile.Linkedin;
                    userBlogsDto[i].Github = approvedBlogs[i].UserProfile.Github;
                    userBlogsDto[i].Name = approvedBlogs[i].UserProfile.Name;
                    userBlogsDto[i].Email = approvedBlogs[i].UserProfile.Email;
                }

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", userBlogsDto);
                return Ok(_responseObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user blogs.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while retrieving user blogs.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
           
        }


        //POST 
        [HttpPost]
        public ActionResult AddUserBlog([FromBody] UserBlogDto userBlogs)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "User not found in database");
                    return NotFound(_responseObject);
                }

                var addBlog = _mapper.Map<UserBlogs>(userBlogs);

                _userBlogsRepository.AddBlogs(userId, addBlog);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "User Blog Added Succesfully");

                return Ok(_responseObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding user blogs.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while adding user blogs.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
           

        }


        [HttpPut("{blogId}")]
        public ActionResult UpdateUserBlogs(int blogId, [FromBody] UserBlogDto userBlogs)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "User id not found");
                    return NotFound(_responseObject);
                }

                var updateUserBlog = _mapper.Map<UserBlogs>(userBlogs);

                _userBlogsRepository.updateblogs(userId, blogId, updateUserBlog);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "User Blog Updated succesfully");

                return Ok(_responseObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating user blogs.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while updating user blogs.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
           

        }


        [HttpDelete("{blogId}")]
        public IActionResult DeleteUserBlogs(int blogId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;

                if (userId == null)
                {
                    _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "User not found");
                    return NotFound(_responseObject);
                }

                _userBlogsRepository.removeBlogs(userId, blogId);
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "User Blog Deleted successfully");

                return Ok(_responseObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting user blogs.");

                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "An error occurred while deleting user blogs.");
                return StatusCode(StatusCodes.Status500InternalServerError, _responseObject);
            }
          

        }
    }
}
