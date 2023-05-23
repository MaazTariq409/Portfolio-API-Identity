using AutoMapper;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Repository
{
	public class SkillsRepository : ISkills
	{
		private readonly PorfolioContext _context;
		public SkillsRepository(PorfolioContext context)
		{
			_context = context;

		}
		public IEnumerable<Skills> GetSkillsByUserID(int id)
		{
			return _context.skills.Where(x => x.ProfileID.Equals(id)).ToList();
		}
		public void AddSkillsByUserID(int id, IEnumerable<Skills> skills)
		{
			var users = _context.userProfiles.Include(x => x.Skills).FirstOrDefault(x => x.UserID.Equals(id));
			foreach (var item in skills)
			{
                users.Skills.Add(item);
            }
			_context.SaveChanges();
		}

		public void updateSkillsByUserID(int id, int skillId, Skills skill)
		{
			var user = _context.userProfiles.Include(x => x.Skills).FirstOrDefault(x => x.UserID.Equals(id));
			if (user != null)
			{
                var _Findskill = user.Skills.FirstOrDefault(x => x.Id == skillId);

				if (_Findskill != null)
				{
					_Findskill.SkillName = skill.SkillName;
					_Findskill.SkillLevel = skill.SkillLevel;
				}
				_context.SaveChanges();
			}
		}

		public void removeSkillsByUserID(int id, int skillId)
		{
			var users = _context.userProfiles.Include(x => x.Skills).FirstOrDefault(x => x.UserID.Equals(id));
			if (users != null)
			{
                var skill = users.Skills[skillId];
                if (skill != null)
                {
                    _context.Remove(skill);
                    _context.SaveChanges();
                }
            }
		}
	}
}
