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
            FriendsWith = new List<UserRelation>();
            FriendsOf = new List<UserRelation>();
            Memories = new List<UserMemory>();
        }
        public User(string firstName, string lastName, string email) : this()
        {            
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            

        }
        #endregion

        #region METHODS
        public void AddFriend(User user)
        {
            FriendsWith.Add(new UserRelation(this, user));
            FriendsOf.Add(new UserRelation(user, this));
        }

        public bool AreFriends(User user)
        {
            return FriendsOf.Select(f => f.FriendOf).Any(f => f.UserId == user.UserId) || FriendsOf.Select(f => f.FriendWith).Any(f => f.UserId == user.UserId);
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
            UserMemory um = new UserMemory(this, memory);
            Memories.Add(um);
        }

        public void RemoveMemory(Memory memory)
        {
            UserMemory um = Memories.FirstOrDefault(t => t.Memory == memory);
            Memories.Remove(um);

        }
        #endregion
    }
}
