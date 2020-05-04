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

        public List<UserDTO> Members { get; set; }

        public List<Photo> Photos { get; set; }

        public MemoryDTO()
        {

        }

        public MemoryDTO(int id, string title, string subTitle, DateTime startDate, DateTime endDate, Location location, List<User> members, List<Photo> photos)
        {
            Id = id;
            Title = title;
            SubTitle = subTitle;
            StartDate = startDate;
            EndDate = endDate;
            Location = location;
            Members = new List<UserDTO>();
            Photos = photos;

            if (members != null)
                members.ForEach(el => Members.Add(new UserDTO(el.FirstName, el.LastName, el.Email)));

        }
    }
}
