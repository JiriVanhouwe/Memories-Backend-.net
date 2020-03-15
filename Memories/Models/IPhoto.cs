using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public interface IPhoto
    {
        Photo GetById(int id);
        IEnumerable<Photo> GetAll();
        void Add(Photo photo);
        void Delete(Photo photo);
        void Update(Photo photo);
        void SaveChanges();

    }
}
