using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestLifeCycleServices.Queries.GetReqLifeCycleDependency;
using System.ComponentModel.DataAnnotations;

namespace EndPoint.Portal.Areas.Portal.Models.ViewModels.RequestViewModel
{
    public class ChangeRequestViewModel
    {
        public UserInfoDto UserInfo { get; set; }
        public ServiceRequestDto ServiceRequest { get; set; }
        public List<ServiceRequestLifeCycleDto> ServiceRequestLifeCycles { get; set; }
        public RequestChangeRequest RequestChangeLifeCycle { get; set; }
        public int ProcessUserId { get; set; }
        public int AssignedUserId { get; set; }
        public long ServiceRequestId { get; set; }
        public string ProcessAction { get; set; }

        [Required]
        [Display(Name = "Operation Type")]
        public int StateOperationType { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
