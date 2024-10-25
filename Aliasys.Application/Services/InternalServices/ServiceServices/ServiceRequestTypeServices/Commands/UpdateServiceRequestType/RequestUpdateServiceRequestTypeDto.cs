using Aliasys.Domain.Entities.ServiceEntities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestTypeServices.Commands.UpdateServiceRequestType
{
    public class RequestUpdateServiceRequestTypeDto
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Request Type")]
        public RequestType RequestType { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Brief Name")]
        public string BriefName { get; set; }
    }
}
