using Aliasys.Domain.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliasys.Persistence.Configurations.BaseEntityConfigs
{
    public class BaseEntityConfig<T, K> : IEntityTypeConfiguration<T> where T : BaseEntity<K>
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd().HasColumnOrder(0);
            builder.Property(p => p.CreatedDateTime).IsRequired().HasColumnType("datetime2");
            builder.Property(p => p.UpdatedDateTime).IsRequired().HasColumnType("datetime2");
            builder.Property(p => p.IsDeleted).IsRequired();
            builder.Property(p => p.DeletedDateTime).HasColumnType("datetime2");
        }
    }
}
