using Memories.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.DTOs
{
    public class MemoryDTO
    {
        [Required]
        public int MemoryId { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public string SubTitle { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public Location Location { get; set; }
        public ICollection<Photo> Photos { get; private set; }
        public ICollection<User> Members { get; set; }

        public MemoryDTO()
        {

        }

        public MemoryDTO(int memoryId, string title, string subTitle, DateTime startDate, DateTime endDate, Location location, ICollection<Photo> photos, ICollection<UserMemory> members)
        {
            MemoryId = memoryId;
            Title = title;
            SubTitle = subTitle;
            StartDate = startDate;
            EndDate = endDate;
            Location = location;
            Photos = photos;
            if(members != null)
                Members = members.Select(m => m.User).ToList();
        }
    }
}
