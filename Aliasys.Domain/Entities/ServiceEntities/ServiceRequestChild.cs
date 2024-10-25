using Aliasys.Domain.Entities.BaseEntities;

namespace Aliasys.Domain.Entities.ServiceEntities
{
    public class ServiceRequestChild : BaseEntity<int>
    {
        public override int Id { get; set; }
        public long ParentServiceRequestId_FK { get; set; }
        public long ChildServiceRequestId_FK { get; set; }
    }
}
