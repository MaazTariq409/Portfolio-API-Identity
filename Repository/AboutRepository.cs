using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Repository
{
    public class AboutRepository : IAbout
    {
        private readonly PorfolioContext _context;

        public AboutRepository(PorfolioContext context)
        {
            _context = context;
        }

        public bool AddAbout(int id, UserProfile about)
        {
			var users = _context.userProfiles.FirstOrDefault(x => x.Id == id);
		
            if (users != null)
			{
				new UserProfile()
				{
					ProfileUrl = about.ProfileUrl,
					Introduction = about.Introduction,
					Description = about.Description,
					Name = about.Name,
					Dob = about.Dob,
					Email = about.Email,
					Linkedin = about.Linkedin,
					Github = about.Github,
					PhoneNo = about.PhoneNo,
					Address = about.Address,
					Language = about.Language,
					Gender = about.Gender
				};

				_context.SaveChanges();
            }
            return false;
        }

        public bool checkAbout(int id)
        {
			var User = _context.userProfiles.FirstOrDefault(x => x.Id == id);

            if (User == null)
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

        public void updateAbout(int id, AboutDto about)
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
