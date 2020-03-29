using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public class User
    {
        #region PROPERTIES
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public ICollection<User> Friends { get; private set; }
        public ICollection<UserMemory> Memories { get; private set; }
        #endregion


        #region CONSTRUCTOR
        public User()
        {

        }
        public User(string firstName, string lastName, string email)
        {            
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Friends = new List<User>();
            Memories = new List<UserMemory>();
        }
        #endregion

        #region METHODS
        public void AddFriend(User user)
        {
            Friends.Add(user);
        }

        public void AddMemory(Memory memory)
        {
            Memories.Add(new UserMemory(this, memory));
        }

        public void RemoveMemory(Memory memory)
        {
            UserMemory um = Memories.FirstOrDefault(t => t.Memory == memory);
            Memories.Remove(um);

        }
        #endregion
    }
}
