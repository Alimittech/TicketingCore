using Aliasys.Domain.Entities.ServiceEntities;
using Aliasys.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Aliasys.Persistence.Configurations.ServiceConfigs
{
    public class ServiceRequestConfig : IEntityTypeConfiguration<ServiceRequest>
    {
        public void Configure(EntityTypeBuilder<ServiceRequest> builder)
        {
            builder.ToTable("ServiceRequest", "srv");
            builder.HasKey(p => p.Id);
            builder.HasOne<User>().WithMany().HasForeignKey(p => p.OwnerUserId_FK);
            builder.HasOne<ServiceCategory>().WithMany().HasForeignKey(p => p.ServiceCategoryId_FK).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne<ServiceRequestType>().WithMany().HasForeignKey(p => p.ServiceRequestTypeId_FK).OnDelete(DeleteBehavior.NoAction);
            builder.HasIndex(p => p.RequestNumber).IsUnique();

            builder.Property(p => p.Id).HasColumnOrder(0);
            builder.Property(p => p.RequestNumber).IsRequired().HasColumnType("varchar(30)").HasColumnOrder(1);
            builder.Property(p => p.OwnerUserId_FK).IsRequired().HasColumnOrder(2);
            builder.Property(p => p.ServiceCategoryId_FK).IsRequired().HasColumnOrder(3);
            builder.Property(p => p.ServiceRequestTypeId_FK).IsRequired().HasColumnOrder(4);
            builder.Property(p => p.ServicePriority).IsRequired().HasColumnType("nvarchar(50)")
                .HasConversion(new EnumToStringConverter<ServicePriority>()).HasColumnOrder(5);
            builder.Property(p => p.OccurDateTime).IsRequired().HasColumnType("datetime2").HasColumnOrder(6);
            builder.Property(p => p.ServiceAffected).IsRequired().HasColumnOrder(7);
            builder.Property(p => p.ImpactOn).IsRequired().HasColumnType("nvarchar(50)")
                .HasConversion(new EnumToStringConverter<ImpactOn>()).HasColumnOrder(8);
            builder.Property(p => p.Title).IsRequired().HasColumnType("nvarchar(50)").HasColumnOrder(9);
            builder.Property(p => p.CreatedDateTime).HasColumnOrder(10);
            builder.Property(p => p.UpdatedDateTime).HasColumnOrder(11);
            builder.Property(p => p.IsDeleted).HasColumnOrder(12);
            builder.Property(p => p.DeletedDateTime).HasColumnOrder(13);
        }
    }
}
