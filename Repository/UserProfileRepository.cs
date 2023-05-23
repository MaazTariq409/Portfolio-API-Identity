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

            //if (identityUser != null)
            //{
            //    userProfile.Name = userProfileDetails.Name;
            //    userProfile.Email = userProfileDetails.Email;
            //    userProfile.PhoneNo = userProfileDetails.PhoneNo;
            //    userProfile.Dob = userProfileDetails.Dob;
            //    userProfile.Description = userProfileDetails.Description;
            //    userProfile.ProfileUrl = userProfileDetails.ProfileUrl;
            //    userProfile.Github = userProfileDetails.Github;
            //    userProfile.Linkedin = userProfileDetails.Linkedin;
            //    userProfile.Gender = userProfileDetails.Gender;
            //    userProfile.Address = userProfileDetails.Address;
            //    userProfile.Introduction = userProfileDetails.Introduction;
            //    userProfile.Language = userProfileDetails.Language;

            //    _context.SaveChanges();

            //    return true;
            //}else
            //return false;
        //}

        public bool checkAbout(int id)
        {
			var users = _context.identityManuals.FirstOrDefault(x => x.Id == "id");

			// var User = _context.userProfiles.FirstOrDefault(x => x.Id == id);

            if (users == null)
            {
                return false;
            }
            return true;
        }

        public UserProfile GetAbout(int id)
        {

			var UserAbout = _context.userProfiles.FirstOrDefault(x => x.Id == id);

            return UserAbout;
        }

        public void removeAbout(int id, int aboutId)
        {
            var users = _context.userProfiles.FirstOrDefault(x => x.Id == id);
            if (users != null)
            {
                _context.Remove(users);
                _context.SaveChanges();
            }
        }

        public void updateAbout(int id, UserProfileDto about)
        {
			var UserAbout = _context.userProfiles.FirstOrDefault(x => x.Id == id);

            if (UserAbout != null)
            {
                UserAbout.Name = about.Name;
                UserAbout.Email = about.Email;
                UserAbout.PhoneNo = about.PhoneNo;
                UserAbout.Dob = about.Dob;
                UserAbout.Description = about.Description;
                UserAbout.ProfileUrl = about.ProfileUrl;
                UserAbout.Github = about.Github;
                UserAbout.Linkedin = about.Linkedin;
                UserAbout.Gender = about.Gender;
                UserAbout.Address = about.Address;
                UserAbout.Introduction = about.Introduction;
                UserAbout.Language = about.Language;

                _context.SaveChanges();
            }
        }
    }
}
