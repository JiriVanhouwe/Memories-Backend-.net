using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public class Location
    {
        #region PROPERTIES
        public int LocationId { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }
        #endregion


        #region CONSTRUCTOR

        public Location()
        {

        }
        public Location(string country, string city)
        {
            Country = country;
            City = city;
        } 
        #endregion
    }
}
