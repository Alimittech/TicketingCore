using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestLifeCycleServices.Queries.GetReqLifeCycleDependency;

namespace EndPoint.Portal.Areas.Portal.Models.ViewModels.RequestLifeCycleViewModel
{
    public class ChangeReqLifeCycleViewModel
    {
        public UserInfoDto UserInfo { get; set; }
        public ServiceRequestDto ServiceRequest { get; set; }
        public List<ServiceRequestLifeCycleDto> ServiceRequestLifeCycles { get; set; }
        public RequestChangeReqLifeCycle RequestChangeLifeCycle { get; set; }
        public int ProcessUserId { get; set; }
    }
}
