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
    }
}
