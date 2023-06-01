using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Repository.Repository_Interface
{
    public interface IUserServiceGig
    {
        public IEnumerable<UserServiceGig> GetServiceGigByUserId (string id);
        public IEnumerable<UserServiceGig> GetAllGig();
        public IEnumerable<UserServiceGig> GetAllGigByServiceId(int serviceId);

        public IEnumerable<UserProfile> GetAbout();

        //public IEnumerable<UserServiceGig> GetAboutWithkeyword(string tag);

        public void AddServiceGig (string id, int serviceGigId , UserServiceGig userServiceGig);


        public void removeServiceGig(string id, int serviceGigId);

        public void removeServiceGigRequest(string userId, int serviceGigId);

        public void updateServiceGig(string id, int serviceGigId, UserServiceGig serviceGig);

        public void updateServiceGigRequest(string id, int serviceGigId, UserServiceGig serviceGig);
    }
}
