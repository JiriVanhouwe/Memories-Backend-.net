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
    [Route("api/friends")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
       /* private readonly IUserRepository _userRepository;

        public FriendsController(IUserRepository context)
        {
            _userRepository = context;
        }

        //GET api/friends
        /// <summary>
        /// Get a user's friends.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>User's friends.</returns>
        [HttpGet("{id}")]
        public IEnumerable<User> GetFriends(int id)
        {
            return _userRepository.GetFriends(id);
        }*/
    }
}