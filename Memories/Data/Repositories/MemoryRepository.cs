using Memories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Data.Repositories
{
    public class MemoryRepository : IMemoryRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly DbSet<Memory> _memories;
        private readonly DbSet<User> _users;

        public MemoryRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _memories = dbContext.Memories;
            _users = dbContext.Users;
        }

        public void Add(Memory memory)
        {
            _memories.Add(memory);
        }

        public void Delete(Memory memory)
        {
            _memories.Remove(memory);
        }

        public IEnumerable<Memory> GetAll(int id)
        {
            User user = _users.Include(u => u.Memories).ThenInclude(um => um.Memory).ThenInclude(m => m.Location).SingleOrDefault(u => u.UserId == id);

            return user.Memories.Select(u => u.Memory).ToList();
        }

        public Memory GetById(int id)
        {
            return _memories.Include(m => m.Location).Include(m => m.Photos).Include(m => m.Members).ThenInclude(m => m.User).SingleOrDefault(m => m.MemoryId == id);
        } //.Include(m => m.Members).ThenInclude(m => m.Memory) => lukt niet: cycle    .Include(m => m.Members).ThenInclude(m => m.User)

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Memory memory)
        {
            _memories.Update(memory);
        }
    }
}
