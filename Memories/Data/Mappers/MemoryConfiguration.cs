using Memories.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Data.Mappers
{
    internal class MemoryConfiguration : IEntityTypeConfiguration<Memory>
    {
        public void Configure(EntityTypeBuilder<Memory> builder)
        {
            builder.ToTable("Memory");
            builder.HasKey(t => t.MemoryId);
            builder.Property(t => t.StartDate).IsRequired();
            builder.Property(t => t.EndDate).IsRequired();
            builder.Property(t => t.Title).IsRequired().HasMaxLength(50);

            builder.HasOne(t => t.Location).WithMany().IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(t => t.Photos).WithOne().OnDelete(DeleteBehavior.Cascade);
            //builder.HasMany(t => t.Members).WithOne().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
