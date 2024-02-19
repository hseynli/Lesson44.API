using _03.Repository.Entities;
using _03.Repository.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _03.Repository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_userRepository.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            _userRepository.Add(user);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        public IActionResult Put([FromBody] User user)
        {
            _userRepository.Update(user);
            return Ok();
        }
    }
}
