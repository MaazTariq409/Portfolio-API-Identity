using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Repository.Repository_Interface
{
	public interface ISkills
	{
		public IEnumerable<Skills> GetSkillsByUserID(string id);

		public void AddSkillsByUserID(string id, IEnumerable<Skills> skills);

		public void updateSkillsByUserID (string id, int skillId, Skills skill);

		public void updateSkillsRequest(string id, int skillId, Skills skill);

        public void removeSkillsByUserID(string id, int skillId);

		public void removeSkillsRequest(string id, int skillId);
    }
}
