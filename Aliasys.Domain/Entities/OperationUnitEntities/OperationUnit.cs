using Aliasys.Domain.Entities.BaseEntities;

namespace Aliasys.Domain.Entities.OperationUnitEntities
{
    public class OperationUnit:BaseEntity<int>
    {
        public override int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
    }
}
