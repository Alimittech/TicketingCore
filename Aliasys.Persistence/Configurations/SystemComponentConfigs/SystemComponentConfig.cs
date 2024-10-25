using Aliasys.Domain.Entities.SystemComponentEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliasys.Persistence.Configurations.SystemComponentConfigs
{
    public class SystemComponentConfig : IEntityTypeConfiguration<SystemComponent>
    {
        public void Configure(EntityTypeBuilder<SystemComponent> builder)
        {
            builder.ToTable("System", "stm");
            builder.HasKey(x => x.Id);
            //builder.HasOne<SystemComponent>().WithMany().HasForeignKey(x => x.ParentId_FK).OnDelete(DeleteBehavior.NoAction);
            //builder.HasIndex(x => x.Name).IsUnique();

            builder.Property(x => x.Id).HasColumnOrder(0);
            builder.Property(x => x.ParentSystemId).IsRequired().HasColumnOrder(1);
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(100)").HasColumnOrder(2);
            builder.Property(x => x.Description).HasColumnType("nvarchar(max)").HasColumnOrder(3);
            builder.Property(x => x.CreatedDateTime).HasColumnOrder(4);
            builder.Property(x => x.UpdatedDateTime).HasColumnOrder(5);
            builder.Property(x => x.IsDeleted).HasColumnOrder(6);
            builder.Property(x => x.DeletedDateTime).HasColumnOrder(7);

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasData(new SystemComponent { Id = 1, Name = "Systems", ParentSystemId = 1 },
                            new SystemComponent { Id = 2, Name = "Portfolio Portal", ParentSystemId = 1 },
                            new SystemComponent { Id = 3, Name = "System Settings", ParentSystemId = 1 },
                            new SystemComponent { Id = 4, Name = "Service System", ParentSystemId = 1 },
                            new SystemComponent { Id = 5, Name = "Organization Management", ParentSystemId = 3 },
                            new SystemComponent { Id = 6, Name = "System Management", ParentSystemId = 3 },
                            new SystemComponent { Id = 7, Name = "Service Management", ParentSystemId = 4 },
                            new SystemComponent { Id = 8, Name = "Create Service", ParentSystemId = 4 },
                            new SystemComponent { Id = 9, Name = "Get Service", ParentSystemId = 4 },
                            new SystemComponent { Id = 10, Name = "Organization", ParentSystemId = 5 },
                            new SystemComponent { Id = 11, Name = "Operation Unit", ParentSystemId = 5 },
                            new SystemComponent { Id = 12, Name = "Position", ParentSystemId = 5 },
                            new SystemComponent { Id = 13, Name = "System", ParentSystemId = 6 },
                            new SystemComponent { Id = 14, Name = "Roll Management", ParentSystemId = 6 },
                            new SystemComponent { Id = 15, Name = "User Account", ParentSystemId = 6 });
        }
    }
}
