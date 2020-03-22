using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public interface IUserRepository
    {
        User GetById(int id);
        IEnumerable<User> GetAll();
        void Update(User user);
        void Add(User user);
        void Delete(User user);
        void SaveChanges();
    }
}
