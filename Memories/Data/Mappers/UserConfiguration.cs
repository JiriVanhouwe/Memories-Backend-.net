using Memories.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Data.Mappers
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(t => t.LastName).IsRequired().HasMaxLength(50);
            builder.Property(t => t.Email).IsRequired();

            builder.HasMany(t => t.Memories).WithOne(t => t.Organizer).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(t => t.Friends).WithOne().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
