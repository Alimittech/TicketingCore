using Aliasys.Domain.Entities.BaseEntities;

namespace Aliasys.Domain.Entities.ServiceEntities
{
    public class ServicePhase : BaseEntity<int>
    {
        public override int Id { get; set; }
        public int ServiceRequestTypeId_FK { get; set; }
        public string PhaseName { get; set; }
    }

    //Creation
    //Handle
    //Process
    //Reject
    //Confirm
}
