using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public class Photo
    {
        #region PROPERTIES
        public int PhotoId { get; set; }
        public string BaseString { get; set; }
      // public byte[] img { get; set; }


        #endregion



        #region CONSTRUCTOR
        public Photo()
        {

        }
        
        public Photo(string s)
        {
            BaseString = s;
        }

        /*

        public Photo(byte[] img)
        {
            this.img = img;
            setBase64();
        }

        public void setBase64()
        {
            BaseString = Convert.ToBase64String(img);
        }*/
        #endregion
    }
}
