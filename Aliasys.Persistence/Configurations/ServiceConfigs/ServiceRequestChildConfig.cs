using Aliasys.Domain.Entities.ServiceEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliasys.Persistence.Configurations.ServiceConfigs
{
    public class ServiceRequestChildConfig : IEntityTypeConfiguration<ServiceRequestChild>
    {
        public void Configure(EntityTypeBuilder<ServiceRequestChild> builder)
        {
            builder.ToTable("ServiceRequestChild", "srv");
            builder.HasKey(p => p.Id);
            builder.HasOne<ServiceRequest>().WithMany().HasForeignKey(p => p.ParentServiceRequestId_FK).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne<ServiceRequest>().WithMany().HasForeignKey(p => p.ChildServiceRequestId_FK).OnDelete(DeleteBehavior.NoAction);
            builder.HasIndex(p => new { p.ParentServiceRequestId_FK, p.ChildServiceRequestId_FK }).IsUnique();

            builder.Property(p => p.Id).HasColumnOrder(0);
            builder.Property(p => p.ParentServiceRequestId_FK).IsRequired().HasColumnOrder(1);
            builder.Property(p => p.ChildServiceRequestId_FK).IsRequired().HasColumnOrder(2);
            builder.Property(p => p.CreatedDateTime).HasColumnOrder(3);
            builder.Property(p => p.UpdatedDateTime).HasColumnOrder(4);
            builder.Property(p => p.IsDeleted).HasColumnOrder(5);
            builder.Property(p => p.DeletedDateTime).HasColumnOrder(6);
        }
    }
}
