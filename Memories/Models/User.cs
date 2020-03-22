using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public class User
    {
        #region PROPERTIES
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<User> Friends { get; private set; }
        public ICollection<Memory> Memories { get; private set; }
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
            Memories = new List<Memory>();
        }
        #endregion

        #region METHODS
        public void AddFriend(User user)
        {
            Friends.Add(user);
        }

        public void AddMemory(Memory memory)
        {
            Memories.Add(memory);
        }


        #endregion
    }
}
