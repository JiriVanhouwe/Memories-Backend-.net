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
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository context)
        {
            _userRepository = context;
        }


        //GET api/users
        /// <summary>
        /// Get all the users.
        /// </summary>
        /// <returns>List with all the users.</returns>
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
           return _userRepository.GetAll().OrderBy(u => u.FirstName).ThenBy(u => u.LastName).ToList();
        }

        //GET api/users/id
        /// <summary>
        /// Get a user with a given id.
        /// </summary>
        /// <param name="id">The id of a user.</param>
        /// <returns>The user. </returns>
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            User user = _userRepository.GetById(id);
            if (user == null)
                return NotFound();

            return user;
        }

        //POST api/users
        /// <summary>
        /// Add a new user.
        /// </summary>
        /// <param name="user">The new user.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            _userRepository.Add(user);
            _userRepository.SaveChanges();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        //PUT api/users/id
        /// <summary>
        /// Modifies a user with the given id.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <param name="user"></param>
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, User user)
        {
            if (id != user.Id)
                return BadRequest();

            _userRepository.Update(user);
            _userRepository.SaveChanges();
            return NoContent();
        }

        //DELETE api/users/id
        /// <summary>
        /// Deletes a user with the given id.
        /// </summary>
        /// <param name="id">The id of a user.</param>
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