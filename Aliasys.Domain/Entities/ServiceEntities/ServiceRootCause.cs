using Aliasys.Domain.Entities.BaseEntities;

namespace Aliasys.Domain.Entities.ServiceEntities
{
    public class ServiceRootCause : BaseEntity<int>
    {
        public override int Id { get; set; }
        public string RootCauseName { get; set; }
    }
}
