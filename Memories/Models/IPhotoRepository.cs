using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public interface IPhotoRepository
    {
        Photo GetById(int id);
        void Add(Photo photo);
        void Delete(Photo photo);
        void Update(Photo photo);
        void SaveChanges();

    }
}
