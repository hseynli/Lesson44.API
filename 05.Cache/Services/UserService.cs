using _04.GenericRepository.Models;
using DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace _05.Cache.Services
{
    public class UserService
    {
        AppDbContext db;
        IMemoryCache cache;

        public UserService(AppDbContext context, IMemoryCache memoryCache)
        {
            db = context;
            cache = memoryCache;
        }

        public async Task<User> GetUser(int id)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Keşdən məlumat almağa cəhd edirik
            cache.TryGetValue(id, out User? user);
            // Əgər keşdə məlumat aşkar edilməsə
            if (user == null)
            {
                // verilənlər bazasına müraciət edirik
                user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                // əgər istifadəçi tapılsa, onu keşə əlavə edirik
                if (user != null)
                {
                    Console.WriteLine($"{user.Name} verilənlər bazasından götürüldü");
                    cache.Set(user.Id, user, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }
            else
            {
                Console.WriteLine($"{user.Name} keşdən götürüldü");
            }
            return user;
        }
    }
}
