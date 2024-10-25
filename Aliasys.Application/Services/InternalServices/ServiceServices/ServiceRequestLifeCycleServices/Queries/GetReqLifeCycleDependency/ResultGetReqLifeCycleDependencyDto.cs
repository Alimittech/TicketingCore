namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestLifeCycleServices.Queries.GetReqLifeCycleDependency
{
    public class ResultGetReqLifeCycleDependencyDto
    {
        public UserInfoDto UserInfo { get; set; }
        public ServiceRequestDto ServiceRequest { get; set; }
        public List<ServiceRequestLifeCycleDto> ServiceRequestLifeCycles { get; set; }
    }
}
