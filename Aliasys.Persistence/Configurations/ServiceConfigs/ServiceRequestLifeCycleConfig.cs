using Aliasys.Domain.Entities.ServiceEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliasys.Persistence.Configurations.ServiceConfigs
{
    public class ServiceRequestLifeCycleConfig : IEntityTypeConfiguration<ServiceRequestLifeCycle>
    {
        public void Configure(EntityTypeBuilder<ServiceRequestLifeCycle> builder)
        {
            builder.ToTable("ServiceRequestLifeCycle", "srv");
            builder.HasKey(p => p.Id);
            builder.HasOne<ServiceRequest>().WithMany().HasForeignKey(p => p.ServiceRequestId_FK);
            builder.HasOne<ServiceState>().WithMany().HasForeignKey(p => p.ServiceStateId_FK).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne<ServicePhase>().WithMany().HasForeignKey(p => p.ServicePhaseId_FK).OnDelete(DeleteBehavior.NoAction);

            builder.Property(p => p.Id).HasColumnOrder(0);
            builder.Property(p => p.ServiceRequestId_FK).IsRequired().HasColumnOrder(1);
            builder.Property(p => p.ServiceStateId_FK).IsRequired().HasColumnOrder(2);
            builder.Property(p => p.ServicePhaseId_FK).IsRequired().HasColumnOrder(3);
            builder.Property(p => p.RootCauseId).HasColumnOrder(4);
            builder.Property(p => p.SubCauseId).HasColumnOrder(5);
            builder.Property(p => p.Description).IsRequired().HasColumnType("nvarchar(max)").HasColumnOrder(6);
            builder.Property(p => p.AttachmentFileName).HasColumnType("nvarchar(max)").HasColumnOrder(7);
            builder.Property(p => p.ProcessAction).IsRequired().HasColumnType("varchar(50)").HasColumnOrder(8);
            builder.Property(p => p.ProcessUserId).IsRequired().HasColumnOrder(9);
            builder.Property(p => p.AssignedUserId).IsRequired().HasColumnOrder(10);
            builder.Property(p => p.AssignedGroupId).HasColumnOrder(11);
            builder.Property(p => p.CreatedDateTime).HasColumnOrder(12);
            builder.Property(p => p.UpdatedDateTime).HasColumnOrder(13);
            builder.Property(p => p.IsDeleted).HasColumnOrder(14);
            builder.Property(p => p.DeletedDateTime).HasColumnOrder(15);
        }
    }
}
