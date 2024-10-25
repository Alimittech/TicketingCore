using Aliasys.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliasys.Persistence.Configurations.UserConfigs
{
    public class UserInRollConfig : IEntityTypeConfiguration<UserInRoll>
    {
        public void Configure(EntityTypeBuilder<UserInRoll> builder)
        {
            builder.ToTable("UserInRoll", "usr");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.UserId_FK, x.RollId_FK }).IsUnique();
            builder.HasOne<User>().WithMany().HasForeignKey(x => x.UserId_FK);
            builder.HasOne<UserRoll>().WithMany().HasForeignKey(x => x.RollId_FK);

            builder.Property(x => x.Id).HasColumnOrder(0);
            builder.Property(x => x.UserId_FK).IsRequired().HasColumnOrder(1);
            builder.Property(x => x.RollId_FK).IsRequired().HasColumnOrder(2);
            builder.Property(x => x.CreatedDateTime).HasColumnOrder(3);
            builder.Property(x => x.UpdatedDateTime).HasColumnOrder(4);
            builder.Property(x => x.IsDeleted).HasColumnOrder(5);
            builder.Property(x => x.DeletedDateTime).HasColumnOrder(6);

            builder.HasData(new UserInRoll { Id = 1, UserId_FK = 1, RollId_FK = 4 },
                            new UserInRoll { Id = 2, UserId_FK = 2, RollId_FK = 4 },
                            new UserInRoll { Id = 3, UserId_FK = 3, RollId_FK = 4 },
                            new UserInRoll { Id = 4, UserId_FK = 4, RollId_FK = 1 },
                            new UserInRoll { Id = 5, UserId_FK = 5, RollId_FK = 4 },
                            new UserInRoll { Id = 6, UserId_FK = 6, RollId_FK = 4 },
                            new UserInRoll { Id = 7, UserId_FK = 7, RollId_FK = 4 },
                            new UserInRoll { Id = 8, UserId_FK = 8, RollId_FK = 4 },
                            new UserInRoll { Id = 9, UserId_FK = 9, RollId_FK = 4 },
                            new UserInRoll { Id = 10, UserId_FK = 10, RollId_FK = 4 },
                            new UserInRoll { Id = 11, UserId_FK = 11, RollId_FK = 4 },
                            new UserInRoll { Id = 12, UserId_FK = 12, RollId_FK = 4 },
                            new UserInRoll { Id = 13, UserId_FK = 13, RollId_FK = 4 },
                            new UserInRoll { Id = 14, UserId_FK = 14, RollId_FK = 4 },
                            new UserInRoll { Id = 15, UserId_FK = 15, RollId_FK = 4 },
                            new UserInRoll { Id = 16, UserId_FK = 16, RollId_FK = 4 },
                            new UserInRoll { Id = 17, UserId_FK = 17, RollId_FK = 3 },
                            new UserInRoll { Id = 18, UserId_FK = 18, RollId_FK = 1 },
                            new UserInRoll { Id = 19, UserId_FK = 19, RollId_FK = 3 },
                            new UserInRoll { Id = 20, UserId_FK = 20, RollId_FK = 3 },
                            new UserInRoll { Id = 21, UserId_FK = 21, RollId_FK = 4 },
                            new UserInRoll { Id = 22, UserId_FK = 22, RollId_FK = 4 },
                            new UserInRoll { Id = 23, UserId_FK = 23, RollId_FK = 4 },
                            new UserInRoll { Id = 24, UserId_FK = 24, RollId_FK = 4 },
                            new UserInRoll { Id = 25, UserId_FK = 25, RollId_FK = 4 },
                            new UserInRoll { Id = 26, UserId_FK = 26, RollId_FK = 4 },
                            new UserInRoll { Id = 27, UserId_FK = 27, RollId_FK = 4 },
                            new UserInRoll { Id = 28, UserId_FK = 28, RollId_FK = 4 },
                            new UserInRoll { Id = 29, UserId_FK = 29, RollId_FK = 4 },
                            new UserInRoll { Id = 30, UserId_FK = 30, RollId_FK = 4 },
                            new UserInRoll { Id = 31, UserId_FK = 31, RollId_FK = 4 },
                            new UserInRoll { Id = 32, UserId_FK = 32, RollId_FK = 4 },
                            new UserInRoll { Id = 33, UserId_FK = 33, RollId_FK = 4 },
                            new UserInRoll { Id = 34, UserId_FK = 34, RollId_FK = 4 },
                            new UserInRoll { Id = 35, UserId_FK = 35, RollId_FK = 4 },
                            new UserInRoll { Id = 36, UserId_FK = 36, RollId_FK = 4 },
                            new UserInRoll { Id = 37, UserId_FK = 37, RollId_FK = 4 },
                            new UserInRoll { Id = 38, UserId_FK = 38, RollId_FK = 4 },
                            new UserInRoll { Id = 39, UserId_FK = 39, RollId_FK = 4 },
                            new UserInRoll { Id = 40, UserId_FK = 40, RollId_FK = 4 },
                            new UserInRoll { Id = 41, UserId_FK = 41, RollId_FK = 4 },
                            new UserInRoll { Id = 42, UserId_FK = 42, RollId_FK = 4 },
                            new UserInRoll { Id = 43, UserId_FK = 43, RollId_FK = 4 },
                            new UserInRoll { Id = 44, UserId_FK = 44, RollId_FK = 4 },
                            new UserInRoll { Id = 45, UserId_FK = 45, RollId_FK = 4 },
                            new UserInRoll { Id = 46, UserId_FK = 46, RollId_FK = 4 },
                            new UserInRoll { Id = 47, UserId_FK = 47, RollId_FK = 4 },
                            new UserInRoll { Id = 48, UserId_FK = 48, RollId_FK = 4 },
                            new UserInRoll { Id = 49, UserId_FK = 49, RollId_FK = 4 },
                            new UserInRoll { Id = 50, UserId_FK = 50, RollId_FK = 4 },
                            new UserInRoll { Id = 51, UserId_FK = 51, RollId_FK = 4 },
                            new UserInRoll { Id = 52, UserId_FK = 52, RollId_FK = 4 },
                            new UserInRoll { Id = 53, UserId_FK = 53, RollId_FK = 4 },
                            new UserInRoll { Id = 54, UserId_FK = 54, RollId_FK = 4 },
                            new UserInRoll { Id = 55, UserId_FK = 55, RollId_FK = 4 });
        }
    }
}
