using AutoMapper;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Repository
{
	public class UserExperienceRepository : IUserExperience
	{
		private readonly PorfolioContext _context;
		public UserExperienceRepository(PorfolioContext context)
		{
			_context = context;

		}

		public IEnumerable<UserExperience> GetUserExperience(string userid)
		{
            var user = _context.identityManuals.Include(x => x.UserProfile.UserExperiences).FirstOrDefault(x => x.Id == userid);
            Thread.Sleep(5000);
            return user.UserProfile.UserExperiences;
		}

		public void AddUserExperience (string userid, IEnumerable<UserExperience> userExperiences)
		{
            var user = _context.identityManuals.Include(x =>x.UserProfile.UserExperiences).FirstOrDefault(x => x.Id == userid);

            foreach (var experience in userExperiences)
            {
                user.UserProfile.UserExperiences.Add(experience);
            }
            _context.SaveChanges();
        }

		public void UpdateUserExperience(string id, int userExperienceid, UserExperience userExperience)
		{
            var user = _context.identityManuals.Include(x => x.UserProfile.UserExperiences).FirstOrDefault(x => x.Id == id);
            
            if (user != null)
            {
                var _Findexperience = user.UserProfile.UserExperiences[userExperienceid];

                if (_Findexperience != null)
				{
					_Findexperience.jobTitle = userExperience.jobTitle;
					_Findexperience.responsibility = userExperience.responsibility;
					_Findexperience.companyName = userExperience.companyName;
                    _Findexperience.duration = userExperience.duration;
					_Findexperience.status = userExperience.status;

                }
				_context.SaveChanges();
			}
		}

        public void UpdateUserExperienceRequest(string id, int userExperienceid, UserExperience userExperience)
        {
            var user = _context.identityManuals.Include(x => x.UserProfile.UserExperiences).FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                var _Findexperience = user.UserProfile.UserExperiences.FirstOrDefault(x => x.Id == userExperienceid);

                if (_Findexperience != null)
                {
                    _Findexperience.jobTitle = userExperience.jobTitle;
                    _Findexperience.responsibility = userExperience.responsibility;
                    _Findexperience.companyName = userExperience.companyName;
                    _Findexperience.duration = userExperience.duration;
                    _Findexperience.status = userExperience.status;
                }
                _context.SaveChanges();
            }
        }

        public void RemoveUserExperience(string id, int userexperienceid)
		{
            var user = _context.identityManuals.Include(x => x.UserProfile.UserExperiences).FirstOrDefault(x => x.Id == id);

            if (user != null)
			{
                var experience = user.UserProfile.UserExperiences[userexperienceid];
                if (experience != null)
                {
                    _context.Remove(experience);
                    _context.SaveChanges();
                }
            }
		}

        public void RemoveUserExperienceRequest(string id, int userexperienceid)
        {
            var user = _context.identityManuals.Include(x => x.UserProfile.UserExperiences).FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                var experience = user.UserProfile.UserExperiences.FirstOrDefault(x => x.Id == userexperienceid);
                if (experience != null)
                {
                    _context.Remove(experience);
                    _context.SaveChanges();
                }
            }
        }
    }
}