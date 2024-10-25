using Aliasys.Domain.Entities.PositionEntitis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliasys.Persistence.Configurations.PositionConfigs
{
    public class PositionConfig : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.ToTable("Position", "org");
            builder.HasKey(x => x.Id);
            //builder.HasIndex(x => x.Name).IsUnique();

            builder.Property(x => x.Id).HasColumnOrder(0);
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(100)").HasColumnOrder(1);
            builder.Property(x => x.CreatedDateTime).IsRequired().HasColumnOrder(2);
            builder.Property(x => x.UpdatedDateTime).IsRequired().HasColumnOrder(3);
            builder.Property(x => x.IsDeleted).IsRequired().HasColumnOrder(4);
            builder.Property(x => x.DeletedDateTime).HasColumnOrder(5);

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasData(new Position { Id = 1, Name = "Chairman" },//رئیس هیئت مدیره
                            new Position { Id = 2, Name = "CEO" },//مدیرعامل
                            new Position { Id = 3, Name = "Deputy CEO" },//قائم مقام مدیرعامل
                            new Position { Id = 4, Name = "Consultant" },//مشاور
                            new Position { Id = 5, Name = "Senior manager" },//مدیر ارشد
                            new Position { Id = 6, Name = "Manager" },//مدیر
                            new Position { Id = 7, Name = "Team Leader" },//سرپرست تیم
                            new Position { Id = 8, Name = "Supervisor" },//سرپرست
                            new Position { Id = 9, Name = "Senior Expert" },//کارشناس ارشد
                            new Position { Id = 10, Name = "Junior Expert" },//کارشناس
                            new Position { Id = 11, Name = "Jobholder" },//کارمند
                            new Position { Id = 12, Name = "Secretary" },//منشی
                            new Position { Id = 13, Name = "Collector" },//تحصیلدار
                            new Position { Id = 14, Name = "Watchman" },//نگهبان
                            new Position { Id = 15, Name = "Worker" },//کارگر
                            new Position { Id = 16, Name = "Junior Developer" },//برنامه نویس
                            new Position { Id = 17, Name = "Senior Developer" },//برنامه نویس ارشد
                            new Position { Id = 18, Name = "Charge" });//مسئول

        }
    }
}
