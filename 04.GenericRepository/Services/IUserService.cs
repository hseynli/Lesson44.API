using _04.GenericRepository.Models;

namespace _04.GenericRepository.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        public User Get(int id);

        public void AddUser(User user);

        public void UpdateUser(User user);
    }
}
