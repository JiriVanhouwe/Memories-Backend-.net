using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Memories.DTOs;
using Memories.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace Memories.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/memories")]
    [ApiController]
    public class MemoriesController : ControllerBase
    {
        private readonly IMemoryRepository _memoryRepository;

        public MemoriesController(IMemoryRepository context)
        {
            _memoryRepository = context;
        }

        //GET api/memories
        /// <summary>
        /// Get a user's memories.
        /// </summary>
        /// <returns>All the memories.</returns>
        [HttpGet]  
        public IEnumerable<Memory> GetMemories() 
        {
            return _memoryRepository.GetAll().OrderBy(m => m.StartDate).ToList();
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

            return new MemoryDTO(memory.MemoryId, memory.Title, memory.SubTitle, memory.StartDate, memory.EndDate, memory.Location, memory.Photos, memory.Members);
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
            _memoryRepository.Add(memoryToCreate);
            _memoryRepository.SaveChanges();

            return CreatedAtAction(nameof(GetMemory), new { id = memoryToCreate.MemoryId }, memoryToCreate);
        }

        //PUT api/memories/id
        /// <summary>
        /// Modifies a memory with the given id.
        /// </summary>
        /// <param name="id">The id of the memory.</param>
        /// <param name="memory">The modified memory.</param>
        [HttpPut("{id}")]
        public IActionResult PutMemory(int id, Memory memory)
        {
            if (id != memory.MemoryId)
                return BadRequest();

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
        [HttpPost]
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