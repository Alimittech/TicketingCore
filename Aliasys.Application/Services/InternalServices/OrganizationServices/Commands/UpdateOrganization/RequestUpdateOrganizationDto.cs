using System.ComponentModel.DataAnnotations;

namespace Aliasys.Application.Services.InternalServices.OrganizationServices.Commands.UpdateOrganization
{
    public class RequestUpdateOrganizationDto
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Parent Organization")]
        public int ParentOrganization { get; set; }//for ParentId_FK

        [Required]
        //[Remote("checkOrganizationName")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Domain Name")]
        public string DomainName { get; set; }

        [Required]
        public int Region { get; set; }//for RegionId_FK

        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}
