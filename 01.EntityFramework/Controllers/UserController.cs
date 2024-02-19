using _01.EntityFramework.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _01.EntityFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationContext db;
        public UserController(ApplicationContext context)
        {
            db = context;
        }
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return db.Users.ToList();
        }

        [HttpGet("{id}")]
        public User Get(int id)
        {
            return db.Users.Where(p => p.Id == id).FirstOrDefault();
        }

        [HttpPost]
        public IActionResult Post(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return Ok(user);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult Put(User user)
        {
            if (ModelState.IsValid)
            {
                db.Update(user);
                db.SaveChanges();
                return Ok(user);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            User user = db.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
            return Ok(user);
        }
    }
}
