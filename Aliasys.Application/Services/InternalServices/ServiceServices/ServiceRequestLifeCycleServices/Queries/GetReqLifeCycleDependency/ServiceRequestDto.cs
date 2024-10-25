namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestLifeCycleServices.Queries.GetReqLifeCycleDependency
{
    public class ServiceRequestDto
    {
        public long ReqId { get; set; }
        public string RequestNumber { get; set; }
        public string OwnerUser { get; set; }
        public string ServiceCategory { get; set; }
        public string ServiceRequestType { get; set; }
        public string ServicePriority { get; set; }
        public DateTime OccuerDateTime { get; set; }
        public bool ServiceAffected { get; set; }
        public string ImpactOn { get; set; }
        public string Title { get; set; }
    }
}
