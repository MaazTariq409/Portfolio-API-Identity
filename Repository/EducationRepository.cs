using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Repository
{
    public class EducationRepository : IEducation
    {
        private readonly PorfolioContext _context;

        public EducationRepository (PorfolioContext context)
        {
            _context = context;
        }

        public IEnumerable<Education> GetDetails(string id)
        {
            var usersEducation = _context.identityManuals.Include(a => a.UserProfile.Education).FirstOrDefault(b => b.Id == id);
            Thread.Sleep(3000);
            return usersEducation.UserProfile.Education;
        }

        public void AddEducation(string id, IEnumerable<Education> Edu)
        {
            var usersEducation = _context.identityManuals.Include(a => a.UserProfile.Education).FirstOrDefault(b => b.Id == id);
            if (usersEducation != null)
            {
                foreach (var item in Edu)
                {
                    usersEducation.UserProfile.Education.Add(item);
                }
                _context.SaveChanges();
            }
        }

        public void removeEducation(string userId, int eduId)
        {
            var usersEducation = _context.identityManuals.Include(a => a.UserProfile.Education).FirstOrDefault(b => b.Id == userId);
            if (usersEducation != null)
            {
                var education = usersEducation.UserProfile.Education[eduId];
                if (education != null)
                {
                    _context.Remove(education);
                    _context.SaveChanges();
                }
            }
        }

        public void removeEducationRequest(string userId, int eduId)
        {
            var usersEducation = _context.identityManuals.Include(a => a.UserProfile.Education).FirstOrDefault(b => b.Id == userId);
            if (usersEducation != null)
            {
                var education = usersEducation.UserProfile.Education.FirstOrDefault(x => x.Id == eduId);
                if (education != null)
                {
                    _context.Remove(education);
                    _context.SaveChanges();
                }
            }
        }

        public void updateEducation(string id, int eduId, Education Edu)
        {
            var usersEducation = _context.identityManuals.Include(a => a.UserProfile.Education).FirstOrDefault(b => b.Id == id);

            if (usersEducation != null)
            {
                var edu = usersEducation.UserProfile.Education[eduId];

                if (edu != null)
                {
                    edu.degreeName = Edu.degreeName;
                    edu.degreeLevel = Edu.degreeLevel;
                    edu.passingYear = Edu.passingYear;
                    edu.institute = Edu.institute;
                    edu.achievement = Edu.achievement;
                    edu.grade = Edu.grade;

                    edu.status= Edu.status;
                    _context.SaveChanges();

                }
                
            }
        }

        public void updateEducationRequest(string id, int eduId, Education Edu)
        {
            var usersEducation = _context.identityManuals.Include(a => a.UserProfile.Education).FirstOrDefault(b => b.Id == id);

            if (usersEducation != null)
            {
                var edu = usersEducation.UserProfile.Education.FirstOrDefault(x => x.Id == eduId);
                if (edu != null)
                {
                    edu.degreeName = Edu.degreeName;
                    edu.degreeLevel = Edu.degreeLevel;
                    edu.passingYear = Edu.passingYear;
                    edu.institute = Edu.institute;
                    edu.achievement = Edu.achievement;
                    edu.grade = Edu.grade;

                    edu.status = Edu.status;
                    _context.SaveChanges();

                }

            }
        }
    }
}
