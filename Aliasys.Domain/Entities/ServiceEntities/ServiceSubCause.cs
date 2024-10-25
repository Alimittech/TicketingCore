using Aliasys.Domain.Entities.BaseEntities;

namespace Aliasys.Domain.Entities.ServiceEntities
{
    public class ServiceSubCause : BaseEntity<int>
    {
        public override int Id { get; set; }
        public string SubCauseName { get; set; }
        public int ParentId { get; set; }
    }
}
