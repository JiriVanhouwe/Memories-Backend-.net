using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.DTOs
{
    public class UserDTO
    {
        [Required]
        public string FirstName { get; set; }
       
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string Email { get; set; }
       
        public ICollection<UserDTO> Friends { get; private set; }
       
        public ICollection<MemoryDTO> Memories { get; private set; }
    }
}
