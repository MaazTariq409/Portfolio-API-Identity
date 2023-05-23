using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Repository.Repository_Interface
{
    public interface IUserProfile
    {
        public UserProfile GetAbout (int id);

        public bool AddAbout (string id, UserProfile about);

        public void removeAbout (int id, int aboutId);

        public void updateAbout (int id, UserProfileDto about);

        public bool checkAbout (int id);
    }
}
