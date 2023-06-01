using AutoMapper;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.DTOs;
using Portfolio_API.DTOs.Admin;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Repository
{
    public class UserServicesRepository : IUserServices
    {
        private readonly PorfolioContext _context;
        public UserServicesRepository(PorfolioContext context)
        {
            _context = context;

        }

        void IUserServices.AddServices(UserServices servicePost)
        {

                _context.userServices.Add(servicePost);
                _context.SaveChanges();
        }

        IEnumerable<UserServices> IUserServices.GetServices()
        {
               return _context.userServices.ToList();
        }

        void IUserServices.removeServices(int serviceId)
        {
            var service = _context.userServices.ToList();
            var delService = service.FirstOrDefault(x => x.ServiceId == serviceId);

            if (delService != null)
            {
                _context.Remove(delService);
                _context.SaveChanges();
            }
        }

        void IUserServices.updateServices(int serviceId, UserServices servicePost)
        {
            var service = _context.userServices.ToList();
            var updateService = service.FirstOrDefault(x => x.ServiceId == serviceId);

            if (updateService != null)
            {
                updateService.Title = servicePost.Title;
                updateService.Description = servicePost.Description;
                updateService.Image = servicePost.Image;
                updateService.status = servicePost.status;

                _context.SaveChanges();
            }
        }

    }

}

