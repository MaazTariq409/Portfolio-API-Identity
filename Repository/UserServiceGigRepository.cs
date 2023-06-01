using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Repository
{
    public class UserServiceGigRepository : IUserServiceGig
    {
        private readonly PorfolioContext _context;
        private readonly IWebHostEnvironment _webHost;

        public UserServiceGigRepository(PorfolioContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        void IUserServiceGig.AddServiceGig(string id, int serviceGigId ,  UserServiceGig userServiceGig)
        {
            var user = _context.identityManuals.Include(x => x.UserProfile.UserServiceGigs).FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                userServiceGig.ServiceId = serviceGigId;
                user.UserProfile.UserServiceGigs.Add(userServiceGig);
                _context.SaveChanges();
            }
        }

        IEnumerable<UserProfile> IUserServiceGig.GetAbout()
        {
            var About = _context.userProfiles.ToList();
            return About;
        }

        IEnumerable<UserServiceGig> IUserServiceGig.GetAllGig()
        {
            var allServices = _context.userServiceGigs.Include(x => x.UserProfile).ToList();
            return allServices;
        }

        IEnumerable<UserServiceGig> IUserServiceGig.GetServiceGigByUserId(string id)
        {
            var user = _context.identityManuals.Include(x => x.UserProfile.UserServiceGigs).FirstOrDefault(x => x.Id == id);
            return user.UserProfile.UserServiceGigs;
        }

        IEnumerable<UserServiceGig> IUserServiceGig.GetAllGigByServiceId(int serviceId)
        {
            var UserGigByServiceId = _context.userServiceGigs.Where(x => x.ServiceId == serviceId).Include(x => x.UserProfile).ToList();
            return UserGigByServiceId;
        }

        void IUserServiceGig.removeServiceGig(string id, int serviceGigId)
        {
            var users = _context.identityManuals.Include(x => x.UserProfile.UserServiceGigs).FirstOrDefault(x => x.Id == id);
            if (users != null)
            {
                var services = users.UserProfile.UserServiceGigs[serviceGigId];
                if (services != null)
                {
                    _context.Remove(services);
                    _context.SaveChanges();
                }
            }
        }

        void IUserServiceGig.removeServiceGigRequest(string userId, int serviceGigId)
        {
            var users = _context.identityManuals.Include(x => x.UserProfile.UserServiceGigs).FirstOrDefault(x => x.Id == userId);
            if (users != null)
            {
                var services = users.UserProfile.UserServiceGigs.FirstOrDefault(x => x.Id == serviceGigId);
                if (services != null)
                {
                    _context.Remove(services);
                    _context.SaveChanges();
                }
            }
        }

        void IUserServiceGig.updateServiceGig(string id, int serviceGigId, UserServiceGig serviceGig)
        {

            var user = _context.identityManuals.Include(x => x.UserProfile.UserServiceGigs).FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                var service1 = user.UserProfile.UserServiceGigs.Where(x => x.Status == "approved").ToList();
                var service = service1[serviceGigId];

                if (service != null)
                {
                    service.Price = serviceGig.Price;
                    service.DateCreated = serviceGig.DateCreated;
                    service.Image = serviceGig.Image;
                    service.Description = serviceGig.Description;
                    service.Title = serviceGig.Title;
                    service.Status = "pending";

                    _context.SaveChanges();
                }
            }
        }

        void IUserServiceGig.updateServiceGigRequest(string id, int serviceGigId, UserServiceGig serviceGig)
        {

            var user = _context.identityManuals.Include(x => x.UserProfile.UserServiceGigs).FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                var service = user.UserProfile.UserServiceGigs.FirstOrDefault(x => x.Id == serviceGigId);
                if (service != null)
                {
                    service.Price = serviceGig.Price;
                    service.DateCreated = serviceGig.DateCreated;
                    service.Image = serviceGig.Image;
                    service.Description = serviceGig.Description;
                    service.Title = serviceGig.Title;
                    service.Status = serviceGig.Status;

                    _context.SaveChanges();

                }

            }
        }

        //public IEnumerable<UserBlogs> GetByUserId(string id)
        //{
        //    var user = _context.identityManuals.Include(x => x.UserProfile.UserBlogs).FirstOrDefault(x => x.Id == id);
        //    return user.UserProfile.UserBlogs;
        //}

        //public IEnumerable<UserBlogs> GetAboutWithkeyword(string tag)
        //{
        //    var formattedTag = tag.Replace(" ", "").ToLower();
        //    var allBlogs = _context.userBlogs
        //        .Where(x => x.tags.Replace(" ", "").ToLower().Contains(formattedTag))
        //        .Include(x => x.UserProfile)
        //        .ToList();
        //    return allBlogs;
        //}

        //public IEnumerable<UserBlogs> GetAll()
        //{
        //    var allBlogs = _context.userBlogs.Include(x => x.UserProfile).ToList();
        //    return allBlogs;
        //}

        //public IEnumerable<UserProfile> GetAbout()
        //{
        //    var About = _context.userProfiles.ToList();
        //    return About;
        //}


        //public void AddBlogs(string id, UserBlogs userBlogs)
        //{
        //    var user = _context.identityManuals.Include(x => x.UserProfile.UserBlogs).FirstOrDefault(x => x.Id == id);

        //    if (user != null)
        //    {

        //        //foreach (var item in userBlogs)
        //        //{
        //        user.UserProfile.UserBlogs.Add(userBlogs);
        //        //}
        //        _context.SaveChanges();
        //    }
        //}

        //public void removeBlogs(string id, int blogId)
        //{
        //    var users = _context.identityManuals.Include(x => x.UserProfile.UserBlogs).FirstOrDefault(x => x.Id == id);
        //    if (users != null)
        //    {
        //        var blogs = users.UserProfile.UserBlogs[blogId];
        //        if (blogs != null)
        //        {
        //            _context.Remove(blogs);
        //            _context.SaveChanges();
        //        }
        //    }
        //}

        //public void removeBlogsRequest(string userId, int blogId)
        //{
        //    var users = _context.identityManuals.Include(x => x.UserProfile.UserBlogs).FirstOrDefault(x => x.Id == userId);
        //    if (users != null)
        //    {
        //        var blog = users.UserProfile.UserBlogs.FirstOrDefault(x => x.Id == blogId);
        //        if (blog != null)
        //        {
        //            _context.Remove(blog);
        //            _context.SaveChanges();
        //        }
        //    }
        //}

        //public void updateblogs(string id, int blogsId, UserBlogs userBlogs)
        //{
        //    var user = _context.identityManuals.Include(x => x.UserProfile.UserBlogs).FirstOrDefault(x => x.Id == id);

        //    if (user != null)
        //    {

        //        //if (user.UserProfile.UserBlogs[blogsId].imageUrl != null)
        //        //{
        //        //    string wwwrootpath = _webHost.WebRootPath;
        //        //    var oldImagePath = Path.Combine(wwwrootpath, user.UserProfile.UserBlogs[blogsId].imageUrl.TrimStart('\\'));
        //        //    if (System.IO.File.Exists(oldImagePath))
        //        //    {
        //        //        int retryCount = 1; // Number of retries
        //        //        int retryDelay = 500; // Delay between retries in milliseconds

        //        //        for (int i = 0; i < retryCount; i++)
        //        //        {
        //        //            try
        //        //            {
        //        //                System.IO.File.Delete(oldImagePath);
        //        //                break; // Exit the loop if deletion is successful
        //        //            }
        //        //            catch (System.IO.IOException)
        //        //            {
        //        //                // File is locked, wait for a short delay and then retry
        //        //                Thread.Sleep(retryDelay);
        //        //            }
        //        //        }
        //        //    }
        //        //}
        //        var blog1 = user.UserProfile.UserBlogs.Where(x => x.status == "approved").ToList();
        //        var blog = blog1[blogsId];

        //        if (blog != null)
        //        {
        //            blog.title = userBlogs.title;
        //            blog.dateCreated = userBlogs.dateCreated;
        //            blog.imageUrl = userBlogs.imageUrl;
        //            blog.content = userBlogs.content;
        //            blog.tags = userBlogs.tags;
        //            blog.status = "pending";

        //            //   blog = userBlogs;

        //            _context.SaveChanges();
        //        }

        //    }
        //}

        //public void updateBlogsRequest(string id, int blogId, UserBlogs userBlogs)
        //{
        //    var user = _context.identityManuals.Include(x => x.UserProfile.UserBlogs).FirstOrDefault(x => x.Id == id);

        //    if (user != null)
        //    {
        //        var blog = user.UserProfile.UserBlogs.FirstOrDefault(x => x.Id == blogId);
        //        if (blog != null)
        //        {
        //            blog.title = userBlogs.title;
        //            blog.status = userBlogs.status;
        //            blog.dateCreated = userBlogs.dateCreated;
        //            blog.imageUrl = userBlogs.imageUrl;
        //            blog.content = userBlogs.content;
        //            blog.tags = userBlogs.tags;
        //            blog.status = userBlogs.status;

        //            _context.SaveChanges();

        //        }

        //    }
        //}
    }
}
