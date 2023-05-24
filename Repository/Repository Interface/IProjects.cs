using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Repository.Repository_Interface
{
	public interface IProjects
    {
		public IEnumerable<UserProjects> GetProjectsByUserID(string id);
		public void AddProjectsByUserID(string id, IEnumerable<UserProjects> projects);
		public void updateProjectsByUserID(string id, int projectId, UserProjects projects);
		public void removeProjectsByUserID(string id, int projectId);


	}
}
