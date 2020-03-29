using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public class UserRelation
    {
        public User FriendOf { get; set; }
        public User FriendWith { get; set; }

        public int FriendOfId { get; set; }
        public int FriendWithId { get; set; }

        protected UserRelation()
        {

        }

        public UserRelation(User friendOf, User friendWith) : this()
        {
            FriendOf = friendOf;
            FriendWith = friendWith;
            FriendOfId = friendOf.UserId;
            FriendWithId = friendWith.UserId;
        }
    }
}
