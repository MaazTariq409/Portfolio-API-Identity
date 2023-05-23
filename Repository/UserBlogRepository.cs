using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Repository
{
    public class UserBlogRepository : IUserBlogs
    {
        private readonly PorfolioContext _context;

        public UserBlogRepository (PorfolioContext context)
        {
            _context = context;
        }

        public IEnumerable<UserBlogs> GetByUserId(int id)
        {
            var allBlogs = _context.userBlogs.Where(x => x.userId == id).Include(x => x.user.About).ToList();
            return allBlogs;
        }

        public IEnumerable<UserBlogs> GetAll()
        {
            var allBlogs = _context.userBlogs.Include(x => x.user.About).ToList();
            return allBlogs;
        }

        public IEnumerable<About> GetAbout()
        {
            var About = _context.about.ToList();
            return About;
        }

        public void AddBlogs(int id, IEnumerable<UserBlogs> userBlogs)
        {
            var user = _context.user.Include(x => x.userBlogs).FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                foreach (var item in userBlogs)
                {
                    user.userBlogs.Add(item);
                }
                _context.SaveChanges();
            }
        }

        public void removeBlogs(int id, int blogId)
        {
            var users = _context.user.Include(x => x.userBlogs).FirstOrDefault(x => x.Id == id);
            if (users != null)
            {
                var blogs = users.userBlogs[blogId];
                if (blogs != null)
                {
                    _context.Remove(blogs);
                    _context.SaveChanges();
                }
            }
        }

        public void removeBlogsRequest(int userId, int blogId)
        {
            var users = _context.user.Include(x => x.userBlogs).FirstOrDefault(x => x.Id == userId);
            if (users != null)
            {
                var blog = users.userBlogs.FirstOrDefault(x => x.Id == blogId);
                if (blog != null)
                {
                    _context.Remove(blog);
                    _context.SaveChanges();
                }
            }
        }

        public void updateblogs(int id, int blogsId, UserBlogs userBlogs)
        {
            var user = _context.user.Include(x => x.userBlogs).FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                var blog = user.userBlogs[blogsId];
                if (blog != null)
                {
                    blog.title = userBlogs.title;
                    blog.dateCreated = userBlogs.dateCreated;
                    blog.imageUrl = userBlogs.imageUrl;
                    blog.content = userBlogs.content;
                    blog.tags = userBlogs.tags;
                    blog.status = "pending";

                 //   blog = userBlogs;

                    _context.SaveChanges();
                }
            }
        }

        public void updateBlogsRequest(int id, int blogId, UserBlogs userBlogs)
        {
            var user = _context.user.Include(x => x.userBlogs).FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                var blog = user.userBlogs.FirstOrDefault(x => x.Id == blogId);
                if (blog != null)
                {
                    blog.title = userBlogs.title;
                    blog.status = userBlogs.status;
                    blog.dateCreated = userBlogs.dateCreated;
                    blog.imageUrl = userBlogs.imageUrl;
                    blog.content = userBlogs.content;
                    blog.tags = userBlogs.tags;
                    blog.status = userBlogs.status;

                    _context.SaveChanges();

                }

            }
        }
    }
}
