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
       
        //POST
        //PUT
        //DELETE
    }
}