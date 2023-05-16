﻿using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<Education> GetDetails(int id)
        {
            //var user = _context.user.Include(x => x.Education).FirstOrDefault(user => user.Id == id);
            //var EducationDetails = user.Education.ToList();

            var education = _context.educations.Where(x => x.UserID == id).ToList();
            return education;
        }

        public void AddEducation(int id, IEnumerable<Education> Edu)
        {
            var user = _context.user.Include(x => x.Education).FirstOrDefault(x => x.Id == id );
            if (user != null)
            {
                foreach (var item in Edu)
                {
                    user.Education.Add(item);
                }
                _context.SaveChanges();
            }
        }

        public void removeEducation(int userId, int eduId)
        {
            var users = _context.user.Include(x => x.Education).FirstOrDefault(x => x.Id == userId);
            if (users != null)
            {
                var education = users.Education[eduId];
                if (education != null)
                {
                    _context.Remove(education);
                    _context.SaveChanges();
                }
            }
        }

        public void updateEducation(int id, int eduId, Education Edu)
        {
            var user = _context.user.Include(x => x.Education).FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                var edu = user.Education[eduId];
                if (edu != null)
                {
                    edu.degreeName = Edu.degreeName;
                    edu.degreeLevel = Edu.degreeLevel;
                    edu.passingYear = Edu.passingYear;
                    edu.institute = Edu.institute;
                    edu.achievement = Edu.achievement;
                    edu.grade = Edu.grade;
                }
                _context.SaveChanges();
            }
        }
    }
}
