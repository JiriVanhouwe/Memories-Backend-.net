﻿using Memories.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Data.Mappers
{
    internal class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Location");
            builder.Property(t => t.LocationId).ValueGeneratedOnAdd();

            builder.HasKey(t => t.LocationId);
            builder.Property(t => t.City).IsRequired().HasMaxLength(50);
            builder.Property(t => t.Country).IsRequired().HasMaxLength(50);
        }
    }
}
