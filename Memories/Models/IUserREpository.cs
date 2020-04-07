using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public interface IUserRepository
    {
        User GetById(int id);
        User GetByEmail(string email);
      //  IEnumerable<User> GetFriends(int id);
        void Update(User user);
        void Add(User user);
        void Delete(User user);
        void SaveChanges();
    }
}
