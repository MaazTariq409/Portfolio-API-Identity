using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;
using System.Net;
using System.Xml.Linq;

namespace Portfolio_API.Repository
{
    public class UserProfileRepository : IUserProfile
    {
        private readonly PorfolioContext _context;

        public UserProfileRepository(PorfolioContext context)
        {
            _context = context;
        }

        public bool AddAbout(string id, UserProfile userProfileDetails)
        {
            var identityUser = _context.identityManuals.FirstOrDefault(x => x.Id == id);

            if (identityUser != null)
            {
                identityUser.UserProfile = userProfileDetails;
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }



        public bool checkAbout(string id)
        {
			var users = _context.identityManuals.FirstOrDefault(x => x.Id == id);
            if (users == null)
            {
                return false;
            }
            return true;
        }

        public UserProfile GetAbout(string id)
        {

			var UserAbout = _context.userProfiles.FirstOrDefault(x => x.UserID == id);

            return UserAbout;
        }

        public void removeAbout(string id, int aboutId)
        {
            var users = _context.identityManuals.Include(x => x.UserProfile).FirstOrDefault(x => x.Id == id);
            if (users != null)
            {
                _context.Remove(users);
                _context.SaveChanges();
            }
        }

        public void updateAbout(string id, UserProfileDto about)
        {
			var UserAbout = _context.identityManuals.Include(x => x.UserProfile).FirstOrDefault(x => x.Id == id);

            if (UserAbout != null)
            {
                UserAbout.UserProfile.Name = about.Name;
                UserAbout.UserProfile.Email = about.Email;
                UserAbout.UserProfile.PhoneNo = about.PhoneNo;
                UserAbout.UserProfile.Dob = about.Dob;
                UserAbout.UserProfile.Description = about.Description;
                UserAbout.UserProfile.ProfileUrl = about.ProfileUrl;
                UserAbout.UserProfile.Github = about.Github;
                UserAbout.UserProfile.Linkedin = about.Linkedin;
                UserAbout.UserProfile.Gender = about.Gender;
                UserAbout.UserProfile.Address = about.Address;
                UserAbout.UserProfile.Introduction = about.Introduction;
                UserAbout.UserProfile.Language = about.Language;

                _context.SaveChanges();
            }
        }

        public IEnumerable<IdentityManual> getUsers()
        {
            var users = _context.identityManuals
            .Include(about => about.UserProfile).ToList();
            return users;
        }

        public IdentityManual getUserPendingRequests(string id)
        {
            var users = _context.identityManuals
            .Include(exp => exp.UserProfile.UserExperiences)
            .Include(skill => skill.UserProfile.Skills)
            .Include(edu => edu.UserProfile.Education)
            .Include(blog => blog.UserProfile.UserBlogs)
            .Include(prod => prod.UserProfile.UserProducts)
            .Include(service => service.UserProfile.UserServiceGigs)
            .FirstOrDefault(x =>x.Id == id);
            return users;
        }
    }
}
