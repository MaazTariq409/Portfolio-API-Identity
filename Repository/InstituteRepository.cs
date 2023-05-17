using AutoMapper;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;
using System.Diagnostics.Metrics;

namespace Portfolio_API.Repository
{
	public class InstituteRepository : IInstitute
	{
        private readonly PorfolioContext _context;
        public InstituteRepository(PorfolioContext context)
        {
            _context = context;

        }

        public IEnumerable<UserInstitute> GetInstitutes()
        {
            return _context.userInstitute.ToList();
        }


        public void AddInstitute(UserInstitute institute)
        {
            _context.userInstitute.Add(institute);
            _context.SaveChanges();
        }

        public void updateInstitute(int instituteId, UserInstitute instituteData)
        {
            var institute = _context.userInstitute.ToList();
            var instituteUpdate = institute[instituteId];

            if (instituteUpdate != null)
            {
                instituteUpdate.Name = instituteData.Name;
                instituteUpdate.BranchNo = instituteData.BranchNo;
                instituteUpdate.Website = instituteData.Website;

                _context.SaveChanges();
            }
        }

        public void removeInstitute(int instituteId)
        {
            var institute = _context.userInstitute.ToList();
            var instituteDel = institute[instituteId];

            if (instituteDel != null)
            {
                _context.Remove(instituteDel);
                _context.SaveChanges();
            }
        }
    }
}
