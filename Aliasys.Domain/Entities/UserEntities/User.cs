using Aliasys.Domain.Entities.BaseEntities;

namespace Aliasys.Domain.Entities.UserEntities
{
    public class User : BaseEntity<int>
    {
        public override int Id { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ExtentionNumber { get; set; }
        public int? PersonCode { get; set; }
        public bool IsActive { get; set; } = false;
        public string? ImageName { get; set; }
    }
}
