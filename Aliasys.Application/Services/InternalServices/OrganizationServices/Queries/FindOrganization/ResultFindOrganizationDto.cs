using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aliasys.Application.Services.InternalServices.OrganizationServices.Queries.FindOrganization
{
    public class ResultFindOrganizationDto
    {
        public int Id { get; set; }
        public SelectListItem ParentOrganization { get; set; }
        public string Name { get; set; }
        public string DomainName { get; set; }
        public SelectListItem Region { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
