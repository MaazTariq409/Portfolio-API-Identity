using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Repository.Repository_Interface
{
	public interface IProjects
    {
		public IEnumerable<UserProjects> GetProjectsByUserID(int id);
		public void AddProjectsByUserID(int id, IEnumerable<UserProjects> projects);
		public void updateProjectsByUserID(int id, int projectId, UserProjects projects);
		public void removeProjectsByUserID(int id, int projectId);


	}
}
