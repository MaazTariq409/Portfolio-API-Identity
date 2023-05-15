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

		public IEnumerable<UserExperience> GetUserExperience(int userid)
		{
			return _context.userExperiences.Where(x => x.userID == userid).ToList();
		}

		public void AddUserExperience (int userid, IEnumerable<UserExperience> userExperiences)
		{
			var users = _context.user.Include(x => x.UserExperiences).FirstOrDefault(x => x.Id == userid);
			foreach (var experience in userExperiences)
			{
                users.UserExperiences.Add(experience);
            }
			_context.SaveChanges();
		}

		public void UpdateUserExperience(int id, int userExperienceid, UserExperience userExperience)
		{
			var user = _context.user.Include(x => x.UserExperiences).FirstOrDefault(x => x.Id == id);
			if (user != null)
			{
				var _Findexperience = user.UserExperiences.FirstOrDefault(x => x.Id == userExperienceid);

				if (_Findexperience != null)
				{
					_Findexperience.jobTitle = userExperience.jobTitle;
					_Findexperience.responsibility = userExperience.responsibility;
					_Findexperience.companyName = userExperience.companyName;
                    _Findexperience.duration = userExperience.duration;

                }
				_context.SaveChanges();
			}
		}

		public void RemoveUserExperience(int id, int userexperienceid)
		{
			var users = _context.user.Include(x => x.UserExperiences).FirstOrDefault(x => x.Id == userexperienceid);
			if (users != null)
			{
                var experience = users.UserExperiences.FirstOrDefault(x => x.Id == userexperienceid);
                if (experience != null)
                {
                    _context.Remove(experience);
                    _context.SaveChanges();
                }
            }
		}
	}
}
