using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Repository
{
    public class UserProfileRepository : IUserProfile
    {
        private readonly PorfolioContext _context;

        public UserProfileRepository(PorfolioContext context)
        {
            _context = context;
        }

        public bool AddAbout(int id, UserProfile userProfile)
        {
			var users = _context.identityManuals.FirstOrDefault(x => x.Id == "id");
		
            if (users != null)
			{
				new UserProfile()
				{
					ProfileUrl = userProfile.ProfileUrl,
					Introduction = userProfile.Introduction,
					Description = userProfile.Description,
					Name = userProfile.Name,
					Dob = userProfile.Dob,
					Email = userProfile.Email,
					Linkedin = userProfile.Linkedin,
					Github = userProfile.Github,
					PhoneNo = userProfile.PhoneNo,
					Address = userProfile.Address,
					Language = userProfile.Language,
					Gender = userProfile.Gender
				};

				_context.SaveChanges();
            }
            return true;
        }

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
