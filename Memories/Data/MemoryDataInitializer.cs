using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Data
{
    public class MemoryDataInitializer
    {
        private readonly ApplicationDbContext _dbContext;

        public MemoryDataInitializer(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public void InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                //seeding the database with recipes, see DBContext               
            }
        }
    }
}
