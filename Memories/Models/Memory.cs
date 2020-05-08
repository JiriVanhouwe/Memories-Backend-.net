using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public class Memory
    {
        #region PROPERTIES
        public int MemoryId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string SubTitle { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public List<Photo> Photos { get; private set; }

        [Required]
        public  Location Location { get; set; }
        public List<UserMemory> Members { get; private set; }
        #endregion

        #region CONSTRUCTOR
        public Memory()
        {
            Photos = new List<Photo>();
            Members = new List<UserMemory>();
        }

        public Memory(string title, string subTitle,DateTime startDate, DateTime endDate, Location location) : this()
        {
            Title = title;
            SubTitle = subTitle;
            StartDate = startDate;
            EndDate = endDate;
            Location = location;
            
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
        public void RemovePhoto(Photo photo)
        {
            Photos.Remove(photo);
        }

        public void AddMember(User user)
        {
            UserMemory um = new UserMemory(user, this);
            Members.Add(um);
        }

        public void RemoveMember(User user)
        {
            UserMemory um = Members.FirstOrDefault(t => t.User == user);
            Members.Remove(um);
        }
        #endregion


    }
}
