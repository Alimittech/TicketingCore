using Aliasys.Domain.Entities.BaseEntities;

namespace Aliasys.Domain.Entities.UserEntities
{
    public class UserInGroup : BaseEntity<int>
    {
        public override int Id { get; set; }
        public int UserGroupId_FK { get; set; }
        public int UserId_FK { get; set; }
    }
}
