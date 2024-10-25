using Aliasys.Domain.Entities.BaseEntities;

namespace Aliasys.Domain.Entities.UserEntities
{
    public class UserGroup : BaseEntity<int>
    {
        public override int Id { get; set; }
        public string GroupName { get; set; }
    }
}
