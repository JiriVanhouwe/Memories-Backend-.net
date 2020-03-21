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
            throw new NotImplementedException();
        }
    }
}
