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
            throw new NotImplementedException();
        }

        public void Delete(Memory memory)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Memory> GetAll()
        {
            throw new NotImplementedException();
        }

        public Memory GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(Memory memory)
        {
            throw new NotImplementedException();
        }
    }
}
