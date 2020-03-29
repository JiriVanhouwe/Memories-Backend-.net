using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.DTOs
{
    public class LocationDTO
    {
        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }
    }
}
