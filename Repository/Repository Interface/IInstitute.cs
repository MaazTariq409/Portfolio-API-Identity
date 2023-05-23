using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Repository.Repository_Interface
{
	public interface IInstitute
    {
        public IEnumerable<UserInstitute> GetInstitutes();
        public void AddInstitute(UserInstitute institute);
        public void updateInstitute(int instituteId, UserInstitute institute);
        public void removeInstitute(int instituteId);
    }
}
