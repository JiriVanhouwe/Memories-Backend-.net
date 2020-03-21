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
            _photos.Add(photo);
        }

        public void Delete(Photo photo)
        {
            _photos.Remove(photo);
        }

        public IEnumerable<Photo> GetAll()
        {
            return _photos.ToList();
        }

        public Photo GetById(int id)
        {
            return _photos.SingleOrDefault(p => p.Id == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Photo photo)
        {
            _photos.Update(photo);
        }
    }
}
