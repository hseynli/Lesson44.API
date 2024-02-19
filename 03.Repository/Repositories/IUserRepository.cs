using _03.Repository.Entities;

namespace _03.Repository.Repositories
{
    public interface IUserRepository
    {
        User GetById(int id);
        IEnumerable<User> GetAll();
        void Add(User entity);
        void Update(User entity);
        void Delete(User entity);
    }
}
