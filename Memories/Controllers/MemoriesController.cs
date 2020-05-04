using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Memories.DTOs;
using Memories.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Memories.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/memories")]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)] //je moet aangemeld zijn om de endpoints te gebruiken
    [ApiController]
    public class MemoriesController : ControllerBase
    {
        private readonly IMemoryRepository _memoryRepository;
        private readonly IUserRepository _userRepository;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public User _loggedInUser;

        public MemoriesController(IMemoryRepository context, IUserRepository userRepo, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _memoryRepository = context;
            _userRepository = userRepo;

            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

            var id = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            User user = _userRepository.GetByEmail(id); 
            _loggedInUser = user;
        }

       /* //GET api/memories
        /// <summary>
        /// Get a user's memories.
        /// </summary>
        /// <returns>All the memories.</returns>
        [HttpGet]  
        public IEnumerable<MemoryWithoutPhotosDTO> GetMemories() 
        {
            List<MemoryWithoutPhotosDTO> result = new List<MemoryWithoutPhotosDTO>();

            User user = _userRepository.UserAndMemories(_loggedInUser.UserId);

            if(user.Memories != null)
            { 
                user.Memories.ForEach(mem => result.Add(new MemoryWithoutPhotosDTO(mem.Memory.MemoryId, mem.Memory.Title, mem.Memory.SubTitle, mem.Memory.StartDate, mem.Memory.EndDate, mem.Memory.Location)));
            }

            return result.OrderBy(m => m.StartDate).ToList();
        }*/

        //GET api/memories
        /// <summary>
        /// Get a user's memories.
        /// </summary>
        /// <returns>All the memories.</returns>
        [HttpGet]
        public IEnumerable<MemoryWithOnePhotoDTO> GetMemories()
        {
            List<MemoryWithOnePhotoDTO> result = new List<MemoryWithOnePhotoDTO>();

            User user = _userRepository.UserAndMemories(_loggedInUser.UserId);

            if (user.Memories != null)
            {
                user.Memories.ForEach(mem => {
                    Photo photo = null;
                    if (mem.Memory.Photos != null)
                        photo = mem.Memory.Photos.First();

                    result.Add(new MemoryWithOnePhotoDTO(mem.Memory.MemoryId, mem.Memory.Title, mem.Memory.SubTitle, mem.Memory.StartDate, mem.Memory.EndDate, mem.Memory.Location, photo));
                });
            }

            return result.OrderBy(m => m.StartDate).ToList();
        }

        //GET api/memory/id
        /// <summary>
        /// Get a user's memory with the given id.
        /// </summary>
        /// <param name="id">The id of the memory.</param>
        /// <returns>The memory.</returns>
        [HttpGet("{id}")]
        public ActionResult<MemoryDTO> GetMemory(int id)
        {
            Memory memory = _memoryRepository.GetById(id);
             if (memory == null) return NotFound();

            List<User> users = new List<User>();

            if (memory.Members != null)
                memory.Members.ForEach(mem => users.Add(_userRepository.GetById(mem.UserId)));

            MemoryDTO result = new MemoryDTO(memory.MemoryId, memory.Title, memory.SubTitle, memory.StartDate, memory.EndDate, memory.Location, users, memory.Photos);

            return result;
        }


        //POST api/memories
        /// <summary>
        /// Add a new memory.
        /// </summary>
        /// <param name="memory">The new memory.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Memory> CreateMemory(MemoryWithoutPhotosDTO memory)
        {
            Memory memoryToCreate = new Memory() {Title = memory.Title, SubTitle = memory.SubTitle, StartDate = memory.StartDate, EndDate = memory.EndDate, Location = memory.Location };
            memoryToCreate.AddMember(_loggedInUser);
            _memoryRepository.Add(memoryToCreate); 
            _memoryRepository.SaveChanges();

            return CreatedAtAction(nameof(GetMemory), new { id = memoryToCreate.MemoryId }, memoryToCreate);
        }

        //POST api/memories/id
        [HttpPost("{id}")]
        public async Task<IActionResult> AddPhoto(IFormFile Image,int id)
        {
            Memory memory = _memoryRepository.GetById(id);

            if (Image != null)
            {
                    using (var stream = new MemoryStream())
                    {
                        await Image.CopyToAsync(stream);
                        memory.AddPhoto(new Photo(Convert.ToBase64String(stream.ToArray())));
                    
                }
                
            }

            _memoryRepository.Update(memory);
            _memoryRepository.SaveChanges();

            return Ok();
        }

        //PUT api/memories/id
        /// <summary>
        /// Modifies a memory with the given id.
        /// </summary>
        /// <param name="memory">The modified memory.</param>
        [HttpPut("{id}")]
        public IActionResult PutMemory(Memory memory)
        {
            if(memory.Photos != null)
            {
                foreach(var photo in memory.Photos)
                {
                    memory.AddPhoto(photo);
                }
            }

            _memoryRepository.Update(memory);
            _memoryRepository.SaveChanges();
            return NoContent();
        }


        //DELETE api/memories/id
        /// <summary>
        /// Deletes a memory with given id.
        /// </summary>
        /// <param name="id">The id of the memory.</param>
        [HttpDelete("{id}")]
         public IActionResult DeleteMemory(int id)
         {
            Memory memory = _memoryRepository.GetById(id);
            if (memory == null)
                return NotFound();

            _memoryRepository.Delete(memory);
            _memoryRepository.SaveChanges();
            return NoContent();
         }


        /*
        //POST api/memories/id/photos
        /// <summary>
        /// Saves a photo into a memory.
        /// </summary>
        /// <param name="memoryId">The memory id.</param>
        /// <param name="Image">The photo.</param>
        [HttpPost("{id}/photos")]
        public async Task<IActionResult> CreatePhoto(int memoryId, List<IFormFile> Image)
        {
            Memory memory = _memoryRepository.GetById(memoryId);

            foreach(var item in Image)
            {
                if(item.Length > 0)
                {
                    using(var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        memory.AddPhoto(new Photo(stream.ToArray()));
                    }
                }
            }
            _memoryRepository.Update(memory);
            _memoryRepository.SaveChanges();
            return NoContent();

        }*/

    }
}