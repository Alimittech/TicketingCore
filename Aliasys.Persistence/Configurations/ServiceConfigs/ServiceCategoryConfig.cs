using Aliasys.Domain.Entities.ServiceEntities;
using Aliasys.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;

namespace Aliasys.Persistence.Configurations.ServiceConfigs
{
    public class ServiceCategoryConfig : IEntityTypeConfiguration<ServiceCategory>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ServiceCategory> builder)
        {
            builder.ToTable("ServiceCategory", "srv");
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.Name).IsUnique();
            builder.HasOne<UserGroup>().WithMany().HasForeignKey(p => p.UserGroupId_FK);

            builder.Property(p => p.Id).HasColumnOrder(0);
            builder.Property(p => p.Name).IsRequired().HasColumnType("nvarchar(50)").HasColumnOrder(1);
            builder.Property(p => p.UserGroupId_FK).IsRequired().HasColumnOrder(2);
            builder.Property(p => p.CreatedDateTime).HasColumnOrder(3);
            builder.Property(p => p.UpdatedDateTime).HasColumnOrder(4);
            builder.Property(p => p.IsDeleted).HasColumnOrder(5);
            builder.Property(p => p.DeletedDateTime).HasColumnOrder(6);

            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.HasData(new ServiceCategory { Id = 1, Name = "ERP", UserGroupId_FK = 1 },
                            new ServiceCategory { Id = 2, Name = "CRM", UserGroupId_FK = 2 });
        }
    }
}
