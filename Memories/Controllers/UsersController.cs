using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Memories.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Memories.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository context)
        {
            _userRepository = context;
        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
           return _userRepository.GetAll().OrderBy(u => u.FirstName).ThenBy(u => u.LastName).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            User user = _userRepository.GetById(id);
            if (user == null)
                return NotFound();

            return user;
        }

        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            _userRepository.Add(user);
            _userRepository.SaveChanges();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult PutUser(int id, User user)
        {
            if (id != user.Id)
                return BadRequest();

            _userRepository.Update(user);
            _userRepository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<User> DeleteUser(int id)
        {
            User user = _userRepository.GetById(id);
            if (user == null)
                return NotFound();

            _userRepository.Delete(user);
            _userRepository.SaveChanges();
            return NoContent();
        }
    }
}