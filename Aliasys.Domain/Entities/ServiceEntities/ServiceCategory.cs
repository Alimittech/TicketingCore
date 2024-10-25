using Aliasys.Domain.Entities.BaseEntities;

namespace Aliasys.Domain.Entities.ServiceEntities
{
    public class ServiceCategory : BaseEntity<int>
    {
        public override int Id { get; set; }
        public string Name { get; set; }//ERP Division, CRM Division
        public int UserGroupId_FK { get; set; }//ERP Group
    }
}
