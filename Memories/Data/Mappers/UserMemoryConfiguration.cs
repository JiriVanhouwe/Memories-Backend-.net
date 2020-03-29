using Memories.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Data.Mappers
{
    public class UserMemoryConfiguration : IEntityTypeConfiguration<UserMemory>
    {
        public void Configure(EntityTypeBuilder<UserMemory> builder)
        {
            builder.ToTable("UserMemory");

            builder.HasKey(t => new { t.UserId, t.MemoryId });

            builder.HasOne(t => t.User).WithMany(t => t.Memories).HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(t => t.Memory).WithMany(t => t.Members).HasForeignKey(t => t.MemoryId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
