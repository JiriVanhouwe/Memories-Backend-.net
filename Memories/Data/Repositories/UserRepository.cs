using Memories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<User> _users;

        public UserRepository(ApplicationDbContext dbContext) 
        {
            _context = dbContext;
            _users = dbContext.Users;
        }

        public void Add(User user)
        {
            _users.Add(user);
        }

        public void Delete(User user)
        {
            _users.Remove(user);
        }

        public User GetByEmail(string email)
        {
            // return _users.Include(u => u.FriendsOf).ThenInclude(u => u.FriendWith)
            //.Include(u => u.Memories).ThenInclude(u => u.Memory).SingleOrDefault(u => u.Email == email);

            return _users.Include(u => u.FriendsOf).ThenInclude(u => u.FriendWith).SingleOrDefault(u => u.Email == email);
        }

        public void Update(User user)
        {
            _users.Update(user);
        }

        public User GetById(int id)
        {
            return _users.SingleOrDefault(u => u.UserId == id);
        }


        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
