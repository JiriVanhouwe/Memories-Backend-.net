using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public class Photo
    {
        #region PROPERTIES
        public int Id { get; set; }
        public string Description { get; set; }

        #endregion



        #region CONSTRUCTOR
        public Photo()
        {

        }

        public Photo(string description = "" )
        {
           
            Description = description;
        } 
        #endregion
    }
}
