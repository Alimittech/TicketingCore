using Aliasys.Domain.Entities.ServiceEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Aliasys.Persistence.Configurations.ServiceConfigs
{
    public class ServiceRequestTypeConfig : IEntityTypeConfiguration<ServiceRequestType>
    {
        public void Configure(EntityTypeBuilder<ServiceRequestType> builder)
        {
            builder.ToTable("ServiceRequestType", "srv");
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.Name).IsUnique();

            builder.Property(p => p.Id).HasColumnOrder(0);
            builder.Property(p => p.RequestType).IsRequired()
                .HasConversion(new EnumToStringConverter<RequestType>()).HasColumnOrder(1);
            builder.Property(p => p.Name).IsRequired().HasColumnType("nvarchar(50)").HasColumnOrder(2);
            builder.Property(p => p.BriefName).IsRequired().HasColumnType("nvarchar(5)").HasColumnOrder(3);
            builder.Property(p => p.CreatedDateTime).HasColumnOrder(4);
            builder.Property(p => p.UpdatedDateTime).HasColumnOrder(5);
            builder.Property(p => p.IsDeleted).HasColumnOrder(6);
            builder.Property(p => p.DeletedDateTime).HasColumnOrder(7);

            builder.HasData(new ServiceRequestType { Id = 1, Name = "Trouble Ticket", BriefName = "TT", RequestType = RequestType.SW_Truoble_Ticket });
        }
    }
}
