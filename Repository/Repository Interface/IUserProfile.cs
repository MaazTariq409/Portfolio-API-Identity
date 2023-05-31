using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Repository.Repository_Interface
{
    public interface IUserProfile
    {
        public UserProfile GetAbout (string id);

        public bool AddAbout (string id, UserProfile about);

        public void removeAbout (string id, int aboutId);

        public void updateAbout (string id, UserProfileDto about);

        public bool checkAbout (string id);

        public IEnumerable<IdentityManual> getUsers();

        public IdentityManual getUserPendingRequests(string id);
    }
}
