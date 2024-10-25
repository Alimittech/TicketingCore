using Aliasys.Domain.Entities.BaseEntities;

namespace Aliasys.Domain.Entities.PositionEntitis
{
    public class Position:BaseEntity<int>
    {
        public override int Id { get; set; }
        public string Name { get; set; }
    }
}
