using Portfolio_API.DTOs;
using Portfolio_API.DTOs.Admin;
using Portfolio_API.Models;

namespace Portfolio_API.Repository.Repository_Interface
{
    public interface IUserServices
    {
        public IEnumerable<UserServices> GetServices();
        public void AddServices(UserServices servicePost);
        public void removeServices(int serviceId);
        public void updateServices(int serviceId, UserServices servicePost);
    }
}
