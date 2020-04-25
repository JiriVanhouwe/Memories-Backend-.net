﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public class Photo
    {
        #region PROPERTIES
        public int PhotoId { get; set; }
        public string Base64String { get; set; }
        //foto toevoegen
        //base64 of als blob in DB
        //of uploaden naar server en daar alles verzamelen in een map

        #endregion



        #region CONSTRUCTOR
        public Photo()
        {

        }

        public Photo(string img )
        {
            Base64String = img;
        } 
        #endregion
    }
}
