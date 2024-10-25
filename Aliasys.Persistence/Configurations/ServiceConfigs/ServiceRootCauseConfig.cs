using Aliasys.Domain.Entities.ServiceEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliasys.Persistence.Configurations.ServiceConfigs
{
    public class ServiceRootCauseConfig : IEntityTypeConfiguration<ServiceRootCause>
    {
        public void Configure(EntityTypeBuilder<ServiceRootCause> builder)
        {
            builder.ToTable("ServiceRootCause", "srv");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.RootCauseName).IsUnique();

            builder.Property(x => x.Id).HasColumnOrder(0);
            builder.Property(x => x.RootCauseName).IsRequired().HasColumnType("nvarchar(50)").HasColumnOrder(1);
            builder.Property(x => x.CreatedDateTime).HasColumnOrder(2);
            builder.Property(x => x.UpdatedDateTime).HasColumnOrder(3);
            builder.Property(x => x.IsDeleted).HasColumnOrder(4);
            builder.Property(x => x.DeletedDateTime).HasColumnOrder(5);

            builder.HasData(new ServiceRootCause { Id = 1, RootCauseName = "Hardware Fault" },
                            new ServiceRootCause { Id = 2, RootCauseName = "Software Fault" },
                            new ServiceRootCause { Id = 3, RootCauseName = "Software Requirement" });
        }
    }
}
