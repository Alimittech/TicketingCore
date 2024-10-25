using Aliasys.Domain.Entities.BaseEntities;

namespace Aliasys.Domain.Entities.SystemComponentEntities
{
    public class SystemComponent : BaseEntity<int>
    {
        public override int Id { get; set; }
        public int ParentSystemId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
