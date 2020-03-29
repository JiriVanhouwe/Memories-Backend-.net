using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public class Location
    {
        #region PROPERTIES
        public int LocationId { get; set; }
        public string Country { get; set; }
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
