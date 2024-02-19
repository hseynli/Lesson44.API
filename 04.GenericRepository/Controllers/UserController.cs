using _04.GenericRepository.Models;
using _04.GenericRepository.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _04.GenericRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_userService.Get(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            _userService.AddUser(user);
            return Created();
        }
    }
}
