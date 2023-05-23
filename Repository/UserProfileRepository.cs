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

        public bool checkAbout(string id)
        {
            var identityUser = _context.identityManuals.FirstOrDefault(x => x.Id == id);

            //var users = _context.identityManuals.FirstOrDefault(x => x.Id == "id");

			// var User = _context.userProfiles.FirstOrDefault(x => x.Id == id);

            if (identityUser == null)
            {
                return false;
            }
            return true;
        }

        public UserProfile GetAbout(string id)
        {
            var getUserAbout = _context.userProfiles.FirstOrDefault(x => x.UserID == id);
			//var UserAbout = _context.userProfiles.FirstOrDefault(x => x.Id == id);

            return getUserAbout;
        }

        public void removeAbout(String id, int aboutId)
        {
            var identityUser = _context.identityManuals.FirstOrDefault(x => x.Id == id);

            if (identityUser != null)
            {
                _context.Remove(identityUser);
                _context.SaveChanges();
            }
        }

        public void updateAbout(string id, UserProfileDto about)
        {
            var identityUser = _context.identityManuals.FirstOrDefault(x => x.Id == id);

            //var UserAbout = _context.userProfiles.FirstOrDefault(x => x.Id == id);

            if (identityUser != null)
            {
                identityUser.UserProfile.Name = about.Name;
                identityUser.UserProfile.Email = about.Email;
                identityUser.UserProfile.PhoneNo = about.PhoneNo;
                identityUser.UserProfile.Dob = about.Dob;
                identityUser.UserProfile.Description = about.Description;
                identityUser.UserProfile.ProfileUrl = about.ProfileUrl;
                identityUser.UserProfile.Github = about.Github;
                identityUser.UserProfile.Linkedin = about.Linkedin;
                identityUser.UserProfile.Gender = about.Gender;
                identityUser.UserProfile.Address = about.Address;
                identityUser.UserProfile.Introduction = about.Introduction;
                identityUser.UserProfile.Language = about.Language;

                _context.SaveChanges();
            }
        }
    }
}
