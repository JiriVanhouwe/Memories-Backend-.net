using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Memories.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Memories.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/memories/{memoryId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoRepository _photosRepository;
        private readonly IMemoryRepository _memoryRepository;


        public PhotosController(IPhotoRepository context, IMemoryRepository memRepo)
        {
            _photosRepository = context;
            _memoryRepository = memRepo;
        }

        /// <summary>
        /// Get a photo with a given id from a certain memory.
        /// </summary>
        /// <param name="id">Id of the photo</param>
        /// <returns>The photo.</returns>
        [HttpGet ("{id}")]
        public ActionResult<Photo> GetPhoto(int id)
        {
            Photo photo = _photosRepository.GetById(id);
            if (photo == null)
                return NotFound();

            return photo;
        }

        /// <summary>
        /// Add a new photo to a memory.
        /// </summary>
        /// <param name="photo">The id of the photo.</param>
        /// <param name="memoryId">The id of the memory.</param>
        [HttpPost]
        public ActionResult<Photo> AddPhoto(Photo photo, int memoryId)
        {
            Memory memory = _memoryRepository.GetById(memoryId);
            memory.AddPhoto(photo);
            _memoryRepository.SaveChanges();
           // _photosRepository.Add(photo);               //moet ik foto toevoegen en opslaan, of enkel memory
            // _photosRepository.SaveChanges();

            return CreatedAtAction(nameof(GetPhoto), new { id = photo.Id }, photo);
        }

        /// <summary>
        /// Modifies a photo.
        /// </summary>
        /// <param name="id">The id of the photo.</param>
        /// <param name="photo">The modified photo.</param>
        [HttpPut("{id}")]
        public ActionResult<Photo> PutPhoto(int id, Photo photo)
        {
            if (id != photo.Id)
                return BadRequest();

            _photosRepository.Update(photo);
            _photosRepository.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Deletes a photo with given id.
        /// </summary>
        /// <param name="id">The id of the photo.</param>
        [HttpDelete("{id}")]
        public ActionResult<Photo> DeletePhoto(int id)
        {
            Photo photo = _photosRepository.GetById(id);
            if (photo == null)
                return NotFound();

            _photosRepository.Delete(photo);
            _photosRepository.SaveChanges();

            return NoContent();
        }
    }
}