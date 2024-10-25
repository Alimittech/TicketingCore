using Aliasys.Domain.Entities.ServiceEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliasys.Persistence.Configurations.ServiceConfigs
{
    public class ServiceStateConfig : IEntityTypeConfiguration<ServiceState>
    {
        public void Configure(EntityTypeBuilder<ServiceState> builder)
        {
            builder.ToTable("ServiceState", "srv");
            builder.HasKey(p => p.Id);
            builder.HasOne<ServiceRequestType>().WithMany().HasForeignKey(p => p.ServiceRequestTypeId_FK);
            builder.HasIndex(p => new { p.ServiceRequestTypeId_FK, p.StateName }).IsUnique();

            builder.Property(p => p.Id).HasColumnOrder(0);
            builder.Property(p => p.ServiceRequestTypeId_FK).IsRequired().HasColumnOrder(1);
            builder.Property(p => p.StateName).IsRequired().HasColumnType("nvarchar(50)").HasColumnOrder(2);
            builder.Property(p => p.CreatedDateTime).HasColumnOrder(3);
            builder.Property(p => p.UpdatedDateTime).HasColumnOrder(4);
            builder.Property(p => p.IsDeleted).HasColumnOrder(5);
            builder.Property(p => p.DeletedDateTime).HasColumnOrder(6);

            builder.HasData(new ServiceState { Id = 1, ServiceRequestTypeId_FK = 1, StateName = "Draft" },
                            new ServiceState { Id = 2, ServiceRequestTypeId_FK = 1, StateName = "Running" },
                            new ServiceState { Id = 3, ServiceRequestTypeId_FK = 1, StateName = "Cancelled" },
                            new ServiceState { Id = 4, ServiceRequestTypeId_FK = 1, StateName = "Closed" });
        }
    }
}
