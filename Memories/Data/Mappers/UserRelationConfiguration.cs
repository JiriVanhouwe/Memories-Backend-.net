using Memories.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Data.Mappers
{
    public class UserRelationConfiguration : IEntityTypeConfiguration<UserRelation>
    {
        public void Configure(EntityTypeBuilder<UserRelation> builder)
        {
            builder.ToTable("UserRelation");

            builder.HasKey(t => new {t.FriendOfId, t.FriendWithId });

            builder.HasOne(t => t.FriendOf).WithMany(t => t.FriendsOf).HasForeignKey(t => t.FriendOfId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.FriendWith).WithMany(t => t.FriendsWith).HasForeignKey(t => t.FriendWithId);

            //KEY constraint 'FK_UserRelation_User_FriendWithId' on table 'UserRelation' may cause cycles or multiple cascade paths. 
            //Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints.
        }
    }
}
