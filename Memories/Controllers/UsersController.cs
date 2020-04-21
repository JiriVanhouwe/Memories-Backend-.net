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
    [Route("api/friends")]
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

        //GET api/friends/id
        /// <summary>
        /// Get a user and friends and memories with a given id.
        /// </summary>
        /// <param name="id">The email of a user.</param>
        /// <returns>The user + memories + friends. </returns>
        [HttpGet("{id}")]
        public ActionResult<UserDTOWithFriends> GetUserAndFriends(int id)
        {
            User user = _userRepository.GetById(id);

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

        //POST api/friends
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

            return CreatedAtAction(nameof(GetUserAndFriends), new { id = userToCreate.UserId }, userToCreate);
        }


        //DELETE api/friends/id
        /// <summary>
        /// Deletes a friend of a user.
        /// </summary>
        /// <param name="id">The id of the friend that will be deleted.</param>
        [HttpDelete("{id}")]
        public ActionResult<User> DeleteUser(string id)
        {  //TODO vriend verwijderen: moet je eerst weten wie ingelogd is! 
            User user = _userRepository.GetByEmail(id);
            if (user == null)
                return NotFound();

            _userRepository.Delete(user);
            _userRepository.SaveChanges();
            return NoContent();
        }



    }
}