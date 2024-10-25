using Aliasys.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliasys.Persistence.Configurations.UserConfigs
{
    public class UserGroupConfig : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.ToTable("UserGroup", "usr");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.GroupName).IsUnique();

            builder.Property(x => x.Id).HasColumnOrder(0);
            builder.Property(x => x.GroupName).IsRequired().HasColumnType("nvarchar(50)").HasColumnOrder(1);
            builder.Property(x => x.CreatedDateTime).HasColumnOrder(2);
            builder.Property(x => x.UpdatedDateTime).HasColumnOrder(3);
            builder.Property(x => x.IsDeleted).HasColumnOrder(4);
            builder.Property(x => x.DeletedDateTime).HasColumnOrder(5);

            builder.HasData(new UserGroup { Id = 1, GroupName = "ERP Group" },
                            new UserGroup { Id = 2, GroupName = "CRM Group" },
                            new UserGroup { Id = 3, GroupName = "Technical Group" });
        }
    }
}
