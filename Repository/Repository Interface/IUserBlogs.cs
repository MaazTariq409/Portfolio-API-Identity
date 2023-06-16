using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Repository.Repository_Interface
{
    public interface IUserBlogs
    {
        public IEnumerable<UserBlogs> GetByUserId (string id);
        public IEnumerable<UserBlogs> GetAll();
        public IEnumerable<UserProfile> GetAbout();

        public IEnumerable<UserBlogs> GetAboutWithkeyword(string tag);

        public void AddBlogs (string id, UserBlogs userBlogs);


        public void removeBlogs (string id, int blogId);

        public void removeBlogsRequest(string userId, int blogId);

        public void updateblogs (string id, int blogsId, UserBlogs userBlogs);

        public void updateBlogsRequest(string id, int blogId, UserBlogs userBlogs);

        public IEnumerable<string> GetAllCategories();
    }
}
