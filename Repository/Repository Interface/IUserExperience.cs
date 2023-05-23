using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Repository.Repository_Interface
{
    public interface IUserExperience
    {
        public IEnumerable<UserExperience> GetUserExperience(string userid);

        public void AddUserExperience(string userid, IEnumerable<UserExperience> userExperiences);

        public void RemoveUserExperience(string userid, int removeUserExperienceId);

        public void RemoveUserExperienceRequest(string id, int userexperienceid);

        public void UpdateUserExperience(string id, int removeUserExperienceId, UserExperience userExperience);

        public void UpdateUserExperienceRequest(string id, int userExperienceid, UserExperience userExperience);
    }
}
