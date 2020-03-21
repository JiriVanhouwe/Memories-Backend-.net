﻿using Memories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Data
{
    public class MemoryDataInitializer
    {
        private readonly ApplicationDbContext _dbContext;

        public MemoryDataInitializer(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public void InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                //locaties
                Location gent = new Location("België", "Gent");
                Location antwerpen = new Location("België", "Antwerpen");
                Location madrid = new Location("Spanje", "Madrid");

                //users
                User jiri = new User("Jiri", "Vanhouwe", "jiri.vanhouwe@gmail.com");
                User angelique = new User("Angelique", "Daponte", "angelique.daponte@gmail.com");
                _dbContext.Users.Add(jiri);
                _dbContext.Users.Add(angelique);

                //memories
                Memory reisMadrid = new Memory("Reis Madrid", "Kuieren in de binnenstad", jiri, new DateTime(2019, 5, 15), new DateTime(2019, 5, 20), madrid);
                Memory kajakkenGent = new Memory("Kajakken in Gent", "Gentse wateren overmeesteren", angelique, new DateTime(2019, 6, 17), new DateTime(2019, 6, 17), gent);
                Memory dinerInAmigo = new Memory("Verjaardagsdiner Amigo", "Smullen te Amigo", jiri, new DateTime(2019, 7, 22), new DateTime(2019, 7, 22), gent);
                _dbContext.Memories.Add(reisMadrid);
                _dbContext.Memories.Add(kajakkenGent);
                _dbContext.Memories.Add(dinerInAmigo);

                //photos
                Photo photo1 = new Photo("Selfie Madrid");
                Photo photo2 = new Photo("Op het water");
                Photo photo3 = new Photo("Diner amigo");
                _dbContext.Photos.Add(photo1);
                _dbContext.Photos.Add(photo2);
                _dbContext.Photos.Add(photo3);

                _dbContext.SaveChanges();
            }
        }
    }
}
