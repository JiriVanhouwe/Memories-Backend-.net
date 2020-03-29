using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public class UserMemory
    {
        public User User { get; set; }
        public Memory Memory { get; set; }
        public int MemoryId { get; set; }
        public int UserId { get; set; }

        protected UserMemory()
        {

        }

        public UserMemory(User user, Memory memory) : this()
        {
            User = user;
            Memory = memory;
            UserId = user.UserId;
            MemoryId = memory.MemoryId;
        }

    }
}
