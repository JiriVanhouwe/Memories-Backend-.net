using Memories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Data.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Photo> _photos;

        public PhotoRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _photos = dbContext.Photos;
        }

        public void Add(Photo photo)
        {
            throw new NotImplementedException();
        }

        public void Delete(Photo photo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Photo> GetAll()
        {
            throw new NotImplementedException();
        }

        public Photo GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(Photo photo)
        {
            throw new NotImplementedException();
        }
    }
}
