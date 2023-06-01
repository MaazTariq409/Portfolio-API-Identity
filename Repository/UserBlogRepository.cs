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
        private readonly IWebHostEnvironment _webHost;

        public UserBlogRepository(PorfolioContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        public IEnumerable<UserBlogs> GetByUserId(string id)
        {
            var user = _context.identityManuals.Include(x => x.UserProfile.UserBlogs).FirstOrDefault(x => x.Id == id);
            return user.UserProfile.UserBlogs;
        }

        public IEnumerable<UserBlogs> GetAboutWithkeyword(string tag)
        {
            var formattedTag = tag.Replace(" ", "").ToLower();
            var allBlogs = _context.userBlogs
                .Where(x => x.tags.Replace(" ", "").ToLower().Contains(formattedTag))
                .Include(x => x.UserProfile)
                .ToList();
            return allBlogs;
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


        public void AddBlogs(string id, UserBlogs userBlogs)
        {
            var user = _context.identityManuals.Include(x => x.UserProfile.UserBlogs).FirstOrDefault(x => x.Id == id);

            if (user != null)
            {

                //foreach (var item in userBlogs)
                //{
                user.UserProfile.UserBlogs.Add(userBlogs);
                //}
                _context.SaveChanges();
            }
        }

        public void removeBlogs(string id, int blogId)
        {
            var users = _context.identityManuals.Include(x => x.UserProfile.UserBlogs).FirstOrDefault(x => x.Id == id);
            if (users != null)
            {
                var approvedBlogs = users.UserProfile.UserBlogs.Where(x => x.status == "approved").ToList();
                var blogs = approvedBlogs[blogId];
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

                //if (user.UserProfile.UserBlogs[blogsId].imageUrl != null)
                //{
                //    string wwwrootpath = _webHost.WebRootPath;
                //    var oldImagePath = Path.Combine(wwwrootpath, user.UserProfile.UserBlogs[blogsId].imageUrl.TrimStart('\\'));
                //    if (System.IO.File.Exists(oldImagePath))
                //    {
                //        int retryCount = 1; // Number of retries
                //        int retryDelay = 500; // Delay between retries in milliseconds

                //        for (int i = 0; i < retryCount; i++)
                //        {
                //            try
                //            {
                //                System.IO.File.Delete(oldImagePath);
                //                break; // Exit the loop if deletion is successful
                //            }
                //            catch (System.IO.IOException)
                //            {
                //                // File is locked, wait for a short delay and then retry
                //                Thread.Sleep(retryDelay);
                //            }
                //        }
                //    }
                //}
                var approvedBlogs = user.UserProfile.UserBlogs.Where(x => x.status == "approved").ToList();
                var blog = approvedBlogs[blogsId];
            
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
