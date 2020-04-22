using Memories.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Data
{
    public class MemoryDataInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public MemoryDataInitializer(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _dbContext = context;
            _userManager = userManager;
        }
        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                //locaties
                Location gent = new Location("België", "Gent");
                Location madrid = new Location("Spanje", "Madrid");

                //users
                User jiri = new User("Jiri", "Vanhouwe", "jiri.vanhouwe@gmail.com");
                User angelique = new User("Angelique", "Daponte", "angelique.daponte@gmail.com");
                User frank = new User("Frank", "Deboosere", "frank.deboosere@gmail.com");
                User paul = new User("Paul", "Jambers", "paul.jambers@gmail.com");
                await CreateUser(jiri.Email, "memories");
                await CreateUser(angelique.Email, "memories");
                await CreateUser(frank.Email, "memories");
                await CreateUser(paul.Email, "memories");
                jiri.AddFriend(angelique);
                jiri.AddFriend(frank);
                jiri.AddFriend(paul);
                
                _dbContext.Users.Add(jiri);
                _dbContext.Users.Add(angelique);
                _dbContext.Users.Add(frank);
                _dbContext.Users.Add(paul);


                //memories
                Memory reisMadrid = new Memory("Reis Madrid", "Kuieren in de binnenstad", new DateTime(2019, 5, 15), new DateTime(2019, 5, 20), madrid);
                Memory kajakkenGent = new Memory("Kajakken in Gent", "Gentse wateren overmeesteren", new DateTime(2019, 6, 17), new DateTime(2019, 6, 17), gent);
                Memory dinerInAmigo = new Memory("Verjaardagsdiner Amigo", "Smullen te Amigo",  new DateTime(2019, 7, 22), new DateTime(2019, 7, 22), gent);

                //photos
                Image image = Image.FromFile("C:\\Users\\Admin\\Pictures\\Saved Pictures\\paradise.jpg");
                Image image2 = Image.FromFile("C:\\Users\\Admin\\Pictures\\Saved Pictures\\paradise2.jpg");
                Image image3 = Image.FromFile("C:\\Users\\Admin\\Pictures\\Saved Pictures\\paradise3.jpg");
                var ms = new MemoryStream();
                var ms2 = new MemoryStream();
                var ms3 = new MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                image2.Save(ms2, System.Drawing.Imaging.ImageFormat.Jpeg);
                image3.Save(ms3, System.Drawing.Imaging.ImageFormat.Jpeg);
                var bytes = ms.ToArray();
                var bytes2 = ms2.ToArray();
                var bytes3 = ms3.ToArray();

                Photo photo1 = new Photo(bytes);
                Photo photo2 = new Photo(bytes2);
                Photo photo3 = new Photo(bytes3);
                List<Photo> listPhotos = new List<Photo>();
                listPhotos.Add(photo1);
                listPhotos.Add(photo2);
                listPhotos.Add(photo3);
                _dbContext.Photos.Add(photo1);
                _dbContext.Photos.Add(photo2);
                _dbContext.Photos.Add(photo3);

              
                kajakkenGent.AddPhoto(photo1); //elk een foto
                reisMadrid.AddPhoto(photo2);
                dinerInAmigo.AddPhoto(photo3);

                //meerdere members toevoegen aan een memory
                kajakkenGent.AddMember(jiri);
                kajakkenGent.AddMember(angelique);
                reisMadrid.AddMember(jiri);
                reisMadrid.AddMember(angelique);
                dinerInAmigo.AddMember(paul);
                dinerInAmigo.AddMember(frank);
                dinerInAmigo.AddMember(jiri);
                dinerInAmigo.AddMember(angelique);


                _dbContext.Memories.Add(reisMadrid);
                _dbContext.Memories.Add(kajakkenGent);
                _dbContext.Memories.Add(dinerInAmigo);        

                _dbContext.SaveChanges();
            }
        }

        private async Task CreateUser(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            await _userManager.CreateAsync(user, password);
           
        }
    }
}
