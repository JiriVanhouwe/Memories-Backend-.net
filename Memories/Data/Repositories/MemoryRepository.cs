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

        public MemoryRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _memories = dbContext.Memories;
        }

        public void Add(Memory memory)
        {
            _memories.Add(memory);
        }

        public void Delete(Memory memory)
        {
            _memories.Remove(memory);
        }

        public IEnumerable<Memory> GetAll()
        {
            return _memories.Include(m => m.Location).ToList();
        }

        public Memory GetById(int id)
        {
            return _memories.Include(m => m.Location).Include(m => m.Photos).SingleOrDefault(m => m.Id == id);
        }

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
