using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public class Memory
    {
        #region PROPERTIES
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public User Organizer { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Photo> Photos { get; private set; }
        public Location Location { get; set; }
        #endregion

        #region CONSTRUCTOR
        public Memory()
        {
           
        }

        public Memory(string title, string subTitle, User organizer, DateTime startDate, DateTime endDate, Location location)
        {
            Title = title;
            SubTitle = subTitle;
            Organizer = organizer;
            StartDate = startDate;
            EndDate = endDate;
            Location = location;
            Photos = new List<Photo>();
        }
        #endregion

        #region METHODS
        public void AddPhoto(Photo photo)
        {
            Photos.Add(photo);
        } 

        public void AddMultiplePhotos(List<Photo> list)
        {
            list.ForEach(p => Photos.Add(p));
        }
        #endregion


    }
}
