using Memories.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.DTOs
{
    public class MemoryWithoutPhotosDTO
    {
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

        public MemoryWithoutPhotosDTO()
        {

        }

        public MemoryWithoutPhotosDTO(string title, string subTitle, DateTime startDate, DateTime endDate, Location location)
        {
            Title = title;
            SubTitle = subTitle;
            StartDate = startDate;
            EndDate = endDate;
            Location = location;
        }
    }
}
