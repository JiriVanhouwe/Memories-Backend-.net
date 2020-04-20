using Memories.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.DTOs
{
    public class UserDTOWithFriends
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
        public List<UserDTO> Friends { get; set; }
       // public List<Memory> Memories { get; set; }

        public UserDTOWithFriends()
        {

        }

        public UserDTOWithFriends(string firstName, string lastName, string email, List<User> friends)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Friends = new List<UserDTO>();

            if (friends != null)
                friends.ForEach(el => Friends.Add(new UserDTO(el.FirstName, el.LastName, el.Email)));
        }
    }
}
