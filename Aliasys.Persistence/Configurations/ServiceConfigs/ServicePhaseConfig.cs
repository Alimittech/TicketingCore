using Aliasys.Domain.Entities.ServiceEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliasys.Persistence.Configurations.ServiceConfigs
{
    public class ServicePhaseConfig : IEntityTypeConfiguration<ServicePhase>
    {
        public void Configure(EntityTypeBuilder<ServicePhase> builder)
        {
            builder.ToTable("ServicePhase", "srv");
            builder.HasKey(p => p.Id);
            builder.HasOne<ServiceRequestType>().WithMany().HasForeignKey(p => p.ServiceRequestTypeId_FK);
            builder.HasIndex(p => new { p.ServiceRequestTypeId_FK, p.PhaseName }).IsUnique();

            builder.Property(p => p.Id).HasColumnOrder(0);
            builder.Property(p => p.ServiceRequestTypeId_FK).IsRequired().HasColumnOrder(1);
            builder.Property(p => p.PhaseName).IsRequired().HasColumnType("nvarchar(50)").HasColumnOrder(2);
            builder.Property(p => p.CreatedDateTime).HasColumnOrder(3);
            builder.Property(p => p.UpdatedDateTime).HasColumnOrder(4);
            builder.Property(p => p.IsDeleted).HasColumnOrder(5);
            builder.Property(p => p.DeletedDateTime).HasColumnOrder(6);

            builder.HasData(new ServicePhase { Id = 1, ServiceRequestTypeId_FK = 1, PhaseName = "Creation" },
                            new ServicePhase { Id = 2, ServiceRequestTypeId_FK = 1, PhaseName = "Handle"},
                            new ServicePhase { Id = 3, ServiceRequestTypeId_FK = 1, PhaseName = "Process"},
                            new ServicePhase { Id = 4, ServiceRequestTypeId_FK = 1, PhaseName = "Reject"},
                            new ServicePhase { Id = 5, ServiceRequestTypeId_FK = 1, PhaseName = "Confirm"});
        }
    }
}
