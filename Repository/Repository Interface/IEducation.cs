using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Repository.Repository_Interface
{
    public interface IEducation
    {
        public IEnumerable<Education> GetDetails (string id);

        public void AddEducation (string id, IEnumerable<Education> about);

        public void removeEducation (string id, int eduId);

        public void removeEducationRequest(string userId, int eduId);

        public void updateEducation (string id, int eduId, Education about);

        public void updateEducationRequest(string id, int eduId, Education about);
    }
}
