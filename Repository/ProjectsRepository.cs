using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Repository
{
    public class ProjectsRepository : IProjects
    {
        private readonly PorfolioContext _context;
        public ProjectsRepository(PorfolioContext context)
        {
            _context = context;
        }
        public IEnumerable<UserProjects> GetProjectsByUserID(int id)
        {
            return _context.userProjects.Where(p => p.UserID == id).ToList();
        }
        public void AddProjectsByUserID(int id, IEnumerable<UserProjects> projects)
        {
            var users = _context.user.Include(x => x.UserProjects).FirstOrDefault(p => p.Id == id);

            foreach (var project in projects)
            {
                users.UserProjects.Add(project);
            }
            _context.SaveChanges();
        }

        public void removeProjectsByUserID(int id, int projectId)
        {
            var users = _context.user.Include(x => x.UserProjects).FirstOrDefault(p => p.Id == id);

            if(users != null)
            {
                var project = users.UserProjects.FirstOrDefault(p => p.Id == projectId);
                if(project != null)
                {
                    users.UserProjects.Remove(project);
                    _context.SaveChanges();
                }
            }
        }

        public void updateProjectsByUserID(int id, int projectId, UserProjects projects)
        {
            var users = _context.user.Include(x => x.UserProjects).FirstOrDefault(p => p.Id == id);

            if(users != null)
            {
                var project = users.UserProjects.FirstOrDefault(p => p.Id == projectId);

                if(project != null)
                {
                    project.ProjectTitle = projects.ProjectTitle;
                    project.Description = projects.Description;
                    project.Stack = projects.Stack;
                    project.GitUrl = projects.GitUrl;
                }
                _context.SaveChanges();
            }
        }
    }
}
