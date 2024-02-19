using _03.Repository.DbContexts;
using _03.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace _03.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<User> _dbSet;

        public UserRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<User>();
        }

        public User GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Add(User entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(User entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(User entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
    }
}