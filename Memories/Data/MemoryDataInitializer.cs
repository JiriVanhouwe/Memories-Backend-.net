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
               
                jiri.AddFriend(angelique);
                jiri.AddFriend(frank);
                jiri.AddFriend(paul);
                
                _dbContext.Users.Add(jiri);
                _dbContext.Users.Add(angelique);
                _dbContext.Users.Add(frank);
                _dbContext.Users.Add(paul);

                await CreateUser(jiri.Email, "Memories1");
                await CreateUser(angelique.Email, "Memories1");
                await CreateUser(frank.Email, "Memories1");
                await CreateUser(paul.Email, "Memories1");
                _dbContext.SaveChanges();


                //memories
                Memory reisMadrid = new Memory("Reis Madrid", "Kuieren in de binnenstad", new DateTime(2019, 5, 15), new DateTime(2019, 5, 20), madrid);
                Memory kajakkenGent = new Memory("Kajakken in Gent", "Gentse wateren overmeesteren", new DateTime(2019, 6, 17), new DateTime(2019, 6, 17), gent);
                Memory dinerInAmigo = new Memory("Verjaardagsdiner Amigo", "Smullen te Amigo",  new DateTime(2019, 7, 22), new DateTime(2019, 7, 22), gent);

                //photos
                Image image = Image.FromFile("C:\\Users\\Admin\\Pictures\\Saved Pictures\\paradise.jpg");
                Image image2 = Image.FromFile("C:\\Users\\Admin\\Pictures\\Saved Pictures\\paradise2.jpg");
                Image image3 = Image.FromFile("C:\\Users\\Admin\\Pictures\\Saved Pictures\\paradise3.jpg");

                var ms = new MemoryStream();
                image.Save(ms, image.RawFormat);
                var bytes = ms.ToArray();
                image2.Save(ms, image.RawFormat);
                var bytes2 = ms.ToArray();
                image3.Save(ms, image.RawFormat);
                var bytes3 = ms.ToArray();

                Photo photo1 = new Photo(Convert.ToBase64String(bytes));
                Photo photo2 = new Photo(Convert.ToBase64String(bytes2));
                Photo photo3 = new Photo(Convert.ToBase64String(bytes3));
              
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
           // await _userManager.SetUserNameAsync(user, user.Email);

        }
    }
}
