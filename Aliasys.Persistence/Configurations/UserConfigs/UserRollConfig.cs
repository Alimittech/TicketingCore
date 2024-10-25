using Aliasys.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliasys.Persistence.Configurations.UserConfigs
{
    public class UserRollConfig : IEntityTypeConfiguration<UserRoll>
    {
        public void Configure(EntityTypeBuilder<UserRoll> builder)
        {
            builder.ToTable("UserRoll", "usr");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnOrder(0);
            builder.Property(x => x.RollName).IsRequired().HasColumnType("nvarchar(100)").HasColumnOrder(1);
            builder.Property(x => x.Description).HasColumnType("nvarchar(300)").HasColumnOrder(2);
            builder.Property(x => x.CreatedDateTime).HasColumnOrder(3);
            builder.Property(x => x.UpdatedDateTime).HasColumnOrder(4);
            builder.Property(x => x.IsDeleted).HasColumnOrder(5);
            builder.Property(x => x.DeletedDateTime).HasColumnOrder(6);

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasData(new UserRoll { Id = 1, RollName = "Admin", Description = "Full Access" },
                            new UserRoll { Id = 2, RollName = "Manager", Description = "Manager Access" },
                            new UserRoll { Id = 3, RollName = "Service_Provider", Description = "Service Provider Access" },
                            new UserRoll { Id = 4, RollName = "User", Description = "User Access" });
            #region Comment
            //builder.HasData(new UserRoll { Id = 1, RollName = "CRUD", Description = "Full Access" },
            //                new UserRoll { Id = 2, RollName = "CRU", Description = "Create,Read,Update" },
            //                new UserRoll { Id = 3, RollName = "CRD", Description = "Create,Read,Delete" },
            //                new UserRoll { Id = 4, RollName = "CR", Description = "Create,Read" },
            //                new UserRoll { Id = 5, RollName = "RUD", Description = "Read,Update,Delete" },
            //                new UserRoll { Id = 6, RollName = "RU", Description = "Read,Update" },
            //                new UserRoll { Id = 7, RollName = "RD", Description = "Read,Delete" },
            //                new UserRoll { Id = 8, RollName = "R", Description = "Read Only" },
            //                new UserRoll { Id = 9, RollName = "None", Description = "None" });
            #endregion
        }
    }
}
