using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Repository.Repository_Interface
{
    public interface IEducation
    {
        public IEnumerable<Education> GetDetails (int id);

        public void AddEducation (int id, IEnumerable<Education> about);

        public void removeEducation (int id, int eduId);

        public void updateEducation (int id, int eduId, Education about);
    }
}
