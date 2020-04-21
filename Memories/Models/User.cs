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
        public List<UserRelation> FriendsWith { get; private set; }
        public List<UserRelation> FriendsOf { get; private set; }
        public List<UserMemory> Memories { get; private set; }
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
            FriendsWith = new List<UserRelation>();
            FriendsOf = new List<UserRelation>();
            Memories = new List<UserMemory>();
        }
        #endregion

        #region METHODS
        public void AddFriend(User user)
        {
            FriendsWith.Add(new UserRelation(this, user));
           // FriendsOf.Add(new UserRelation(user, this));
        }

        public void RemoveFriend(User user)
        {
            UserRelation ur = FriendsWith.FirstOrDefault(u => u.FriendWith == user);
            UserRelation ur2 = FriendsOf.FirstOrDefault(u => u.FriendOf == user);
            FriendsWith.Remove(ur);
            FriendsWith.Remove(ur2);
            FriendsOf.Remove(ur);
            FriendsOf.Remove(ur2);
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
