using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Repository
{
    public class UserRepository : IUser
    {
        private readonly PorfolioContext _context;

        public UserRepository(PorfolioContext context)
        {
            _context = context;
        }

        public IEnumerable<User> Users()
        {
            var users = _context.user.ToList();
            return users;
        }

        public User GetById(int id)
        {
            return _context.user.FirstOrDefault(x => x.Id == id);
        }

        public User GetUserById(int id, bool includeProperty = false)
        {
            if(includeProperty)
            {
                return _context.user.Include(x => x.About).FirstOrDefault(x => x.Id == id);
            }
            return _context.user.FirstOrDefault(x => x.Id == id);
        }

        public void addUser(User user)
        {
            _context.user.Add(user);
            _context.SaveChanges();
        }

        public void removeUser(int id)
        {
            var user = _context.user.FirstOrDefault(x => x.Id == id);
            if(user != null)
            {
                _context.user.Remove(user);
                _context.SaveChanges();
            }
        }

        public void updateUser(int id, User user)
        {
            var userFromDB = _context.user.FirstOrDefault(x => x.Id == id);
            if (userFromDB != null)
            {
                userFromDB.username = user.username;
                userFromDB.password = user.password;
                userFromDB.email = user.email;
            }
            _context.SaveChanges();
        }

        public bool validateUser(User user)
        {
            var userFromDb = _context.user.FirstOrDefault(x => x.email == user.email);
            if (userFromDb != null)
            {
                return true;
            }

            return false;
        }
    }
}
