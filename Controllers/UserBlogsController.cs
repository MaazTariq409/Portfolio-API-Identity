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

        public UserBlogsController( IMapper mapper, IUserBlogs userBlogsRepository)
        {
            _mapper = mapper;
            _userBlogsRepository = userBlogsRepository;

        }


        //Get
        [AllowAnonymous]
        [HttpGet("/api/userblogs")]

        public ActionResult<List<UserBlogsDto>> GetAllBlogs()
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

                userBlogsDto[i].ProfileUrl = approvedBlogs[i].user.About.ProfileUrl;
                userBlogsDto[i].linkedin = approvedBlogs[i].user.About.Linkedin;
                userBlogsDto[i].Github = approvedBlogs[i].user.About.Github;
                userBlogsDto[i].Name = approvedBlogs[i].user.About.Name;
                userBlogsDto[i].Email = approvedBlogs[i].user.About.Email;
            }

            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", userBlogsDto);
            return Ok(_responseObject);
        }



        [AllowAnonymous]
        [HttpGet("/api/userblogSearched/{tag}")]

        public ActionResult<List<UserBlogsDto>> GetBlogwithKeyword(string tag)
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

                userBlogsDto[i].ProfileUrl = approvedBlogs[i].user.About.ProfileUrl;
                userBlogsDto[i].linkedin = approvedBlogs[i].user.About.Linkedin;
                userBlogsDto[i].Github = approvedBlogs[i].user.About.Github;
                userBlogsDto[i].Name = approvedBlogs[i].user.About.Name;
                userBlogsDto[i].Email = approvedBlogs[i].user.About.Email;
            }

            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", userBlogsDto);
            return Ok(_responseObject);
        }

        [HttpGet]
        public ActionResult<List<UserBlogsDto>> GetUserBlogs()
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);
            if (userId == 0)
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

                userBlogsDto[i].ProfileUrl = approvedBlogs[i].user.About.ProfileUrl;
                userBlogsDto[i].linkedin = approvedBlogs[i].user.About.Linkedin;
                userBlogsDto[i].Github = approvedBlogs[i].user.About.Github;
                userBlogsDto[i].Name = approvedBlogs[i].user.About.Name;
                userBlogsDto[i].Email = approvedBlogs[i].user.About.Email;
            }

            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "Request Succesfull", userBlogsDto);
            return Ok(_responseObject);
        }


        //POST 
        [HttpPost]
        public ActionResult AddUserBlog([FromBody] IEnumerable<UserBlogDto> userBlogs)
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);
            if (userId == 0)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "User not found in database");
                return NotFound(_responseObject);
            }

            var addBlog = _mapper.Map<IEnumerable<UserBlogs>>(userBlogs);

            _userBlogsRepository.AddBlogs(userId, addBlog);
            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "User Blog Added Succesfully");

            return Ok(_responseObject);

        }


        [HttpPut("{blogId}")]
        public ActionResult UpdateUserExperience(int blogId, [FromBody] UserBlogDto userBlogs)
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);
            if (userId == 0)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "User id not found");
                return NotFound(_responseObject);
            }

            var updateUserBlog = _mapper.Map<UserBlogs>(userBlogs);

            _userBlogsRepository.updateblogs(userId, blogId, updateUserBlog);
            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "User Blog Updated succesfully");

            return Ok(_responseObject);

        }


        [HttpDelete("{blogId}")]
        public IActionResult DeleteUserExperience(int blogId)
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);

            if (userId == 0)
            {
                _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Failure.ToString(), "User not found");
                return NotFound(_responseObject);
            }

            _userBlogsRepository.removeBlogs(userId, blogId);
            _responseObject = ResponseBuilder.GenerateResponse(ResultCode.Success.ToString(), "User Blog Deleted successfully");

            return Ok(_responseObject);

        }
    }
}
