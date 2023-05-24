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

        public IEnumerable<UserBlogs> GetByUserId(string id)
        {
            var user = _context.identityManuals.Include(x => x.UserProfile.UserBlogs).FirstOrDefault(x => x.Id == id);
            return user.UserProfile.UserBlogs;
        }

        public IEnumerable<UserBlogs> GetAll()
        {
            var allBlogs = _context.userBlogs.Include(x => x.UserProfile).ToList();
            return allBlogs;
        }

        public IEnumerable<UserProfile> GetAbout()
        {
            var About = _context.userProfiles.ToList();
            return About;
        }

        public void AddBlogs(string id, IEnumerable<UserBlogs> userBlogs)
        {
            var user = _context.identityManuals.Include(x => x.UserProfile.UserBlogs).FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                foreach (var item in userBlogs)
                {
                    user.UserProfile.UserBlogs.Add(item);
                }
                _context.SaveChanges();
            }
        }

        public void removeBlogs(string id, int blogId)
        {
            var users = _context.identityManuals.Include(x => x.UserProfile.UserBlogs).FirstOrDefault(x => x.Id == id);
            if (users != null)
            {
                var blogs = users.UserProfile.UserBlogs[blogId];
                if (blogs != null)
                {
                    _context.Remove(blogs);
                    _context.SaveChanges();
                }
            }
        }

        public void removeBlogsRequest(string userId, int blogId)
        {
            var users = _context.identityManuals.Include(x => x.UserProfile.UserBlogs).FirstOrDefault(x => x.Id == userId);
            if (users != null)
            {
                var blog = users.UserProfile.UserBlogs.FirstOrDefault(x => x.Id == blogId);
                if (blog != null)
                {
                    _context.Remove(blog);
                    _context.SaveChanges();
                }
            }
        }

        public void updateblogs(string id, int blogsId, UserBlogs userBlogs)
        {
            var user = _context.identityManuals.Include(x => x.UserProfile.UserBlogs).FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                var blog = user.UserProfile.UserBlogs[blogsId];
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

        public void updateBlogsRequest(string id, int blogId, UserBlogs userBlogs)
        {
            var user = _context.identityManuals.Include(x => x.UserProfile.UserBlogs).FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                var blog = user.UserProfile.UserBlogs.FirstOrDefault(x => x.Id == blogId);
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
