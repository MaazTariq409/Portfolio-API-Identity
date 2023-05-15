using Portfolio_API.Models;

namespace Portfolio_API.Repository.Repository_Interface
{
    public interface IUser
    {
        public IEnumerable<User> Users();

        public User GetUserById(int id, bool includeProperty);

        public void addUser(User user);

        public void removeUser(int id);

        public void updateUser(int id, User user);

        public bool validateUser(User user);
    }
}
