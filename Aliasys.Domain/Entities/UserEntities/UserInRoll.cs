using Aliasys.Domain.Entities.BaseEntities;

namespace Aliasys.Domain.Entities.UserEntities
{
    public class UserInRoll : BaseEntity<int>
    {
        public override int Id { get; set; }
        public int UserId_FK { get; set; }
        public int RollId_FK { get; set; }
    }
}
