﻿using System;
using System.Collections.Generic;
using System.Drawing;
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
using Microsoft.EntityFrameworkCore;

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


        /// <summary>
        /// Get memories based on the given filter.
        /// </summary>
        /// <param name="filter">Filter the memories</param>
        /// <returns>Filtered memories</returns>
        [HttpGet]
        public IEnumerable<MemoryWithOnePhotoDTO> GetMemories(string filter = null)
        {
            List<MemoryWithOnePhotoDTO> result = new List<MemoryWithOnePhotoDTO>();

            User user = _userRepository.UserAndMemories(_loggedInUser.UserId);

            if (user.Memories != null)
            {
                user.Memories.ForEach(mem =>
                {
                    Photo photo = null;
                    if (mem.Memory.Photos.Count != 0)
                        photo = mem.Memory.Photos.First();
                    else photo = LoadNoImage();             //standaard foto toevoegen

                    result.Add(new MemoryWithOnePhotoDTO(mem.Memory.MemoryId, mem.Memory.Title, mem.Memory.SubTitle, mem.Memory.StartDate, mem.Memory.EndDate, mem.Memory.Location, photo));
                });
                if (filter != null)
                {
                    return GetByFilter(result, filter);
                }
                else
                    return result.OrderByDescending(m => m.StartDate);
            }
            return result;
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

        /// <summary>
        /// Get the friends who are not part of the memory.
        /// </summary>
        /// <param name="id">id of the memory</param>
        /// <returns>List of friends who are not part of the memory yet.</returns>
        [HttpGet("{id}/add")]
        public IEnumerable<string> GetFriendsNotPartOfTheMemory(int id)
        {
            Memory memory = _memoryRepository.GetById(id);

            List<string> friends = _loggedInUser.FriendsWith.Select(f => f.FriendWith.Email).ToList();
            List<string> userMemory = memory.Members.Select(m => m.User.Email).ToList();

           return friends.Except(userMemory).ToList();
        }


        //POST api/memories
        /// <summary>
        /// Add a new memory.
        /// </summary>
        /// <param name="memory">The new memory.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<MemoryWithoutPhotosDTO> CreateMemory(MemoryWithoutPhotosDTO memory)
        {
            Memory memoryToCreate = new Memory() {Title = memory.Title, SubTitle = memory.SubTitle, StartDate = memory.StartDate, EndDate = memory.EndDate, Location = memory.Location };
            
            _loggedInUser.AddMemory(memoryToCreate);
            _memoryRepository.SaveChanges();

            return memory;   
        }

        //POST api/memories/id
        /// <summary>
        /// Add a photo to a memory.
        /// </summary>
        /// <param name="Image">The photo.</param>
        /// <param name="id">Id of the memory.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Adds a friend to a memory.
        /// </summary>
        /// <param name="email">email of the friend</param>
        /// <param name="id">id of the memory</param>
        /// <returns></returns>
        //PUT api/memories/id/add
        [HttpPut("{id}/add")]
        public IActionResult AddFriendToMemory(string email, int id)
        {
            Memory m = _memoryRepository.GetById(id);

            if (m == null)
                return BadRequest();

            User userToAdd = _userRepository.GetByEmail(email);

            if (userToAdd == null)
                return BadRequest();

            m.AddMember(userToAdd);
            _memoryRepository.SaveChanges();

            return NoContent();

        }

        //PUT api/memories/id/edit
        /// <summary>
        /// Change a memory.
        /// </summary>
        /// <param name="memory">The modified memory</param>
        /// <param name="id">Id of the modified memory</param>
        /// <returns></returns>
        [HttpPut("{id}/edit")]
        public IActionResult PutMemory(Memory memory, int id)
        {
            Memory m = _memoryRepository.GetById(id); 

            if (m == null)
                return BadRequest();

            m.Title = memory.Title;
            m.SubTitle = memory.SubTitle;
            if(memory.Location.City != m.Location.City || memory.Location.Country != m.Location.Country)
                m.Location = memory.Location;
            m.StartDate = memory.StartDate;
            m.EndDate = memory.EndDate;

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

            memory.RemoveMember(_loggedInUser);
            _memoryRepository.SaveChanges();
            return NoContent();
         }
        
        //hulpmethode
        private List<MemoryWithOnePhotoDTO> GetByFilter(List<MemoryWithOnePhotoDTO> mem, string filter)
        {
            return mem.Where(m => m.Title.ToLower().Contains(filter.ToLower()) || m.SubTitle.ToLower().Contains(filter.ToLower()) || m.Location.City.ToLower().Contains(filter.ToLower()) || m.Location.Country.ToLower().Contains(filter.ToLower())).OrderByDescending(m => m.StartDate).ToList();
        }

        private Photo LoadNoImage()
        {
            Image image = Image.FromFile("./Images/NoImage.jpg");

            var ms = new MemoryStream();
            image.Save(ms, image.RawFormat);
            var bytes = ms.ToArray();

            return new Photo(Convert.ToBase64String(bytes));
        }


    }
}