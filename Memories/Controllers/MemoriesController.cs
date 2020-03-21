using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Memories.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Memories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemoriesController : ControllerBase
    {
        private readonly IMemoryRepository _memoryRepository;

        public MemoriesController(IMemoryRepository context)
        {
            _memoryRepository = context;   
        }

        //GET
        [HttpGet]
        public IEnumerable<Memory> GetMemories()
        {
            return _memoryRepository.GetAll().OrderBy(m => m.StartDate).ToList();
        }

        [HttpGet]
        public ActionResult<Memory> GetMemory(int id)
        {
            Memory memory = _memoryRepository.GetById(id);
            if (memory == null) return NotFound();

            return memory;
        }
        //POST
        //PUT
        //DELETE
    }
}