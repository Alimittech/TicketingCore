namespace Aliasys.Application.Services.InternalServices.OrganizationServices.Queries.GetOrganization.GetOrgFullList
{
    public class RequestGetOrganizationFullListDto
    {
        public int Id { get; set; }
        public string ParentOrganization { get; set; }
        public string Name { get; set; }
        public string DomainName { get; set; }
        public string RegionName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
