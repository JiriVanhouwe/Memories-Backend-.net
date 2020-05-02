using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public interface IMemoryRepository
    {
        Memory GetById(int id);
        IEnumerable<Memory> GetAll(int id);
        void Add(Memory memory);
        void Delete(Memory memory);
        void Update(Memory memory);
        void SaveChanges();
    }
}
