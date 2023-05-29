using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Repository.Repository_Interface
{
    public interface IUserBlogs
    {
        public IEnumerable<UserBlogs> GetByUserId (int id);
        public IEnumerable<UserBlogs> GetAll();
        public IEnumerable<About> GetAbout();



        public void AddBlogs (int id, UserBlogs userBlogs);

        public void removeBlogs (int id, int blogId);

        public void removeBlogsRequest(int userId, int blogId);

        public void updateblogs (int id, int blogsId, UserBlogs userBlogs);

        public void updateBlogsRequest(int id, int blogId, UserBlogs userBlogs);
    }
}
