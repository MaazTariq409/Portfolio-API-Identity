using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Repository.Repository_Interface
{
    public interface IUserBlogs
    {
        public IEnumerable<UserBlogs> GetByUserId (string id);
        public IEnumerable<UserBlogs> GetAll();
        public IEnumerable<UserProfile> GetAbout();



        public void AddBlogs (string id, IEnumerable<UserBlogs> userBlogs);

        public void removeBlogs (string id, int blogId);

        public void removeBlogsRequest(string userId, int blogId);

        public void updateblogs (string id, int blogsId, UserBlogs userBlogs);

        public void updateBlogsRequest(string id, int blogId, UserBlogs userBlogs);
    }
}
