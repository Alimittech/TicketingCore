using Aliasys.Domain.Entities.BaseEntities;

namespace Aliasys.Domain.Entities.UserEntities
{
    public class UserInOrgOpunitPos : BaseEntity<int>
    {
        public override int Id { get; set; }
        public int UserId_FK { get; set; }
        public int OrganizationId_FK { get; set; }
        public int OperationUnitId_FK { get; set; }
        public int PositionId_FK { get; set; }
    }
}
