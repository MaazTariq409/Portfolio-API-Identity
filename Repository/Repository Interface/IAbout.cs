using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Repository.Repository_Interface
{
    public interface IAbout
    {
        public UserProfile GetAbout (int id);

        public bool AddAbout (int id, UserProfile about);

        public void removeAbout (int id, int aboutId);

        public void updateAbout (int id, AboutDto about);

        public bool checkAbout (int id);
    }
}
