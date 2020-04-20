using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Memories.DTOs;
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
        private readonly IMemoryRepository _memoryRepository;

        public UsersController(IUserRepository context, IMemoryRepository memoRepo)
        {
            _userRepository = context;
            _memoryRepository = memoRepo;
        }

        //GET api/users/email
        /// <summary>
        /// Get a user and friends and memories with a given email.
        /// </summary>
        /// <param name="email">The email of a user.</param>
        /// <returns>The user + memories + friends. </returns>
        [HttpGet("{email}")]
        public ActionResult<UserDTOWithFriends> GetUserByEmail(string email)
        {
            User user = _userRepository.GetByEmail(email);

            if (user == null)
                return NotFound();

           List<User> friends = new List<User>();

            if (user.FriendsWith != null)
            {
                Console.WriteLine("NIET LEEG");
                user.FriendsWith.ForEach(friend => friends.Add(_userRepository.GetById(friend.FriendWithId)));
            } else Console.WriteLine("WEL LEEG");
                

            UserDTOWithFriends userWithFriends = new UserDTOWithFriends(user.FirstName, user.LastName, user.Email, friends);

            return userWithFriends;
        }

        //POST api/users
        /// <summary>
        /// Add a new user.
        /// </summary>
        /// <param name="user">The new user.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<User> CreateUser(UserDTO user)
        {
            User userToCreate = new User() {FirstName = user.FirstName, LastName = user.LastName, Email = user.Email };
            _userRepository.Add(userToCreate);
            _userRepository.SaveChanges();

            return CreatedAtAction(nameof(GetUserByEmail), new { email = userToCreate.Email }, userToCreate);
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
            if (id != user.UserId)
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