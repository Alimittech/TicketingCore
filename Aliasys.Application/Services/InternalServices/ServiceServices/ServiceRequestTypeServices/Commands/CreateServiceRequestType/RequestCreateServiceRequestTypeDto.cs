using Aliasys.Domain.Entities.ServiceEntities;
using System.ComponentModel.DataAnnotations;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestTypeServices.Commands.CreateServiceRequestType
{
    public class RequestCreateServiceRequestTypeDto
    {
        [Required]
        [Display(Name="Request Type")]
        public RequestType RequestType { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name="Brief Name")]
        public string BriefName { get; set; }
    }
}
