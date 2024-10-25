using Aliasys.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliasys.Persistence.Configurations.UserConfigs
{
    public class UserInGroupConfig : IEntityTypeConfiguration<UserInGroup>
    {
        public void Configure(EntityTypeBuilder<UserInGroup> builder)
        {
            builder.ToTable("UserInGroup", "usr");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.UserGroupId_FK, x.UserId_FK });
            builder.HasOne<UserGroup>().WithMany().HasForeignKey(x => x.UserGroupId_FK).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne<User>().WithMany().HasForeignKey(x => x.UserId_FK).OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Id).HasColumnOrder(0);
            builder.Property(x => x.UserGroupId_FK).IsRequired().HasColumnOrder(1);
            builder.Property(x => x.UserId_FK).IsRequired().HasColumnOrder(2);
            builder.Property(x => x.CreatedDateTime).HasColumnOrder(3);
            builder.Property(x => x.UpdatedDateTime).HasColumnOrder(4);
            builder.Property(x => x.IsDeleted).HasColumnOrder(5);
            builder.Property(x => x.DeletedDateTime).HasColumnOrder(6);

            builder.HasData(new UserInGroup { Id = 1, UserGroupId_FK = 1, UserId_FK = 4 },
                            new UserInGroup { Id = 2, UserGroupId_FK = 1, UserId_FK = 17 },
                            new UserInGroup { Id = 3, UserGroupId_FK = 1, UserId_FK = 19 },
                            new UserInGroup { Id = 4, UserGroupId_FK = 2, UserId_FK = 18 },
                            new UserInGroup { Id = 5, UserGroupId_FK = 2, UserId_FK = 20 },
                            new UserInGroup { Id = 6, UserGroupId_FK = 2, UserId_FK = 4 });
                            
        }
    }
}
