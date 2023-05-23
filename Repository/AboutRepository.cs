using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Repository
{
    public class AboutRepository : IAbout
    {
        private readonly PorfolioContext _context;
        private readonly IMapper _mapper;


        public AboutRepository(PorfolioContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public bool AddAbout(int id, About about)
        {
            var user = _context.user.Include(x => x.About).FirstOrDefault(x => x.Id == id);
            if (user.About == null)
            {
                user.About = about;
                _context.SaveChanges();
            }
            return false;
        }

        public bool checkAbout(int id)
        {
            var user = _context.user.Include(x =>x.About).FirstOrDefault(x => x.Id == id);
            if (user.About == null)
            {
                return false;
            }
            return true;
        }

        public About GetAbout(int id)
        {
            var UserAbout = _context.about.FirstOrDefault(x => x.UserID == id);
            return UserAbout;
        }

        public void removeAbout(int id, int aboutId)
        {
            var users = _context.user.Include(x => x.About).FirstOrDefault(x => x.Id == id);
            if (users != null)
            {
                _context.Remove(users.About);
                _context.SaveChanges();
            }
        }

        public void updateAbout(int id, AboutDto about)
        {
            //var UserAbout = _context.about.Where(x => x.Id == id).Include(x => x.address).ToList();

            var UserAbout = _context.about.FirstOrDefault(x => x.UserID == id);

            if (UserAbout != null)
            {
                UserAbout.Name = about.Name;
                UserAbout.Email = about.Email;
                UserAbout.PhoneNo = about.PhoneNo;
                UserAbout.Dob = about.Dob;
                UserAbout.Description = about.Description;
                UserAbout.ProfileUrl = about.ProfileUrl;
                UserAbout.Github = about.Github;
                UserAbout.Linkedin = about.Linkedin;
                UserAbout.Gender = about.Gender;
                UserAbout.Address = about.Address;
                UserAbout.Introduction = about.Introduction;
                UserAbout.Language = about.Language;
            }
            else
            {
                var about1 = _mapper.Map<About>(about);
                AddAbout(id, about1);
            }
            _context.SaveChanges();

        }
    }
}
