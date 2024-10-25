using System.ComponentModel.DataAnnotations;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceStateServices.Commands.CreateServiceState
{
    public class RequestCreateServiceStateDto
    {
        [Required]
        [Display(Name="Request Type")]
        public int ServiceRequestTypeId_FK { get; set; }

        [Required]
        [Display(Name="State Name")]
        public string StateName { get; set; }
    }
}
