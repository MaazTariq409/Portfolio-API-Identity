using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Repository.Repository_Interface
{
	public interface ISkills
	{
		public IEnumerable<Skills> GetSkillsByUserID(int id);
		public void AddSkillsByUserID(int id, IEnumerable<Skills> skills);
		public void updateSkillsByUserID (int id, int skillId, Skills skill);
		public void removeSkillsByUserID(int id, int skillId);


	}
}
