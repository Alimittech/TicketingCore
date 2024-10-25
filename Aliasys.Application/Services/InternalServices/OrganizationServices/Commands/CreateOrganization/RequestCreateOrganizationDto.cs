using System.ComponentModel.DataAnnotations;

namespace Aliasys.Application.Services.InternalServices.OrganizationServices.Commands.CreateOrganization
{
    public class RequestCreateOrganizationDto
    {
        [Required]
        [Display(Name = "Parent Organization")]
        public int ParentOrganization { get; set; }//for ParentId_FK

        [Required]
        //[Remote("checkOrganizationName")]
        public string Name { get; set; }

        [Required]
        //[Remote("checkDomainName")]
        public string DomainName { get; set; }

        [Required]
        public int Region { get; set; }//for RegionId_FK

        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}
