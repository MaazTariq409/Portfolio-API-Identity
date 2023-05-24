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
        public IEnumerable<UserProjects> GetProjectsByUserID(string id)
        {
            var usersProject = _context.identityManuals.Include(a => a.UserProfile.UserProjects).FirstOrDefault(b => b.Id == id);
            return usersProject.UserProfile.UserProjects;
        }
        public void AddProjectsByUserID(string id, IEnumerable<UserProjects> projects)
        {
            var usersProject = _context.identityManuals.Include(a => a.UserProfile.UserProjects).FirstOrDefault(b => b.Id == id);

            foreach (var project in projects)
            {
                usersProject.UserProfile.UserProjects.Add(project);
            }
            _context.SaveChanges();
        }

        public void removeProjectsByUserID(string id, int projectId)
        {
            var usersProject = _context.identityManuals.Include(a => a.UserProfile.UserProjects).FirstOrDefault(b => b.Id == id);

            if (usersProject != null)
            {
                var project = usersProject.UserProfile.UserProjects[projectId];
                if(project != null)
                {
                    usersProject.UserProfile.UserProjects.Remove(project);
                    _context.SaveChanges();
                }
            }
        }

        public void updateProjectsByUserID(string id, int projectId, UserProjects projects)
        {
            var usersProject = _context.identityManuals.Include(a => a.UserProfile.UserProjects).FirstOrDefault(b => b.Id == id);

            if (usersProject != null)
            {
                var project = usersProject.UserProfile.UserProjects.FirstOrDefault(p => p.Id == projectId);

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
