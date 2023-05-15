using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Repository.Repository_Interface
{
    public interface IUserExperience
    {
        public IEnumerable<UserExperience> GetUserExperience(int userid);

        public void AddUserExperience(int userid, IEnumerable<UserExperience> userExperiences);

        public void RemoveUserExperience(int userid, int removeUserExperienceId);

        public void UpdateUserExperience(int id, int removeUserExperienceId, UserExperience userExperience);
    }
}
