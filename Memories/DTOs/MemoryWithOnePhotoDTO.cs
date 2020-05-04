using Memories.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.DTOs
{
    public class MemoryWithOnePhotoDTO
    {
        [Required]
        public int Id { get; set; }
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

        [Required]
        public Photo Photo { get; set; }

        public MemoryWithOnePhotoDTO()
        {

        }

        public MemoryWithOnePhotoDTO(int id, string title, string subTitle, DateTime startDate, DateTime endDate, Location location, Photo photo)
        {
            Id = id;
            Title = title;
            SubTitle = subTitle;
            StartDate = startDate;
            EndDate = endDate;
            Location = location;
            Photo = photo;
        }
    }
}
