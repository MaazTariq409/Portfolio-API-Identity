using AutoMapper;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.DTOs;
using Portfolio_API.DTOs.Admin;
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
		public IEnumerable<Skills> GetSkillsByUserID(string id)
		{
            var user = _context.identityManuals.Include(x => x.UserProfile.Skills).FirstOrDefault(x => x.Id == id);

            return user.UserProfile.Skills;
		}
		public void AddSkillsByUserID(string id, IEnumerable<Skills> skills)
		{
            //var user = _context.userProfiles.Include(x => x.UserID == id);
           var users = _context.identityManuals.Include(x => x.UserProfile.Skills).FirstOrDefault(x => x.Id == id);
			foreach (var item in skills)
			{
                users.UserProfile.Skills.Add(item);
            }
			_context.SaveChanges();
		}

		public void updateSkillsByUserID(string id, int skillId, Skills skill)
		{
            var user = _context.identityManuals.Include(x => x.UserProfile.Skills).FirstOrDefault(x => x.Id == id);

           // var user = _context.userProfiles.Include(x => x.Skills).FirstOrDefault(x => x.UserID.Equals(id));
			if (user != null)
			{
                var _Findskill = user.UserProfile.Skills[skillId];

				if (_Findskill != null)
				{
					_Findskill.SkillName = skill.SkillName;
					_Findskill.SkillLevel = skill.SkillLevel;
					_Findskill.status = skill.status;
                    _context.SaveChanges();
                }
			}
		}

        public void updateSkillsRequest(string id, int skillId, Skills skill)
        {
            var user = _context.identityManuals.Include(x => x.UserProfile.Skills).FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                var _Findskill = user.UserProfile.Skills.FirstOrDefault(x => x.Id == skillId);

                if (_Findskill != null)
                {
                    _Findskill.SkillName = skill.SkillName;
                    _Findskill.SkillLevel = skill.SkillLevel;
                    _Findskill.status = skill.status;
                    _context.SaveChanges();
                }
            }
        }

        public void removeSkillsByUserID(string id, int skillId)
		{
            var users = _context.identityManuals.Include(x => x.UserProfile.Skills).FirstOrDefault(x => x.Id == id);

            //var users = _context.userProfiles.Include(x => x.Skills).FirstOrDefault(x => x.UserID.Equals(id));
			if (users != null)
			{
                var skill = users.UserProfile.Skills[skillId];
                if (skill != null)
                {
                    _context.Remove(skill);
                    _context.SaveChanges();
                }
            }
		}

        public void removeSkillsRequest(string id, int skillId)
        {
            var users = _context.identityManuals.Include(x => x.UserProfile.Skills).FirstOrDefault(x => x.Id == id);
            if (users != null)
            {
                var skill = users.UserProfile.Skills.FirstOrDefault(x => x.Id == skillId);
                if (skill != null)
                {
                    _context.Remove(skill);
                    _context.SaveChanges();
                }
            }
        }
    }
}
