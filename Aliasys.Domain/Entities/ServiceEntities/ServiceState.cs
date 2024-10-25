using Aliasys.Domain.Entities.BaseEntities;

namespace Aliasys.Domain.Entities.ServiceEntities
{
    public class ServiceState : BaseEntity<int>
    {
        public override int Id { get; set; }
        public int ServiceRequestTypeId_FK { get; set; }
        public string StateName { get; set; }
    }
    #region Comment
    //Draft
    //Running
    //Cancelled
    //Completed
    #endregion
}
