using Aliasys.Domain.Entities.BaseEntities;

namespace Aliasys.Domain.Entities.OperationUnitEntities
{
    public class OperationUnitDependency : BaseEntity<int>
    {
        public override int Id { get; set; }
        public int OrganizationId_FK { get; set; }
        public int OperationUnitId_FK { get; set; }
        public int ParentOperationUnitId_FK { get; set; }
        public int ManagerId_FK { get; set; }
    }
}
