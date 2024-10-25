namespace Aliasys.Application.Services.InternalServices.OperationUnitDependencyServices.Queries.FindOpunitDep
{
    public class ResultFindOpunitDepDto
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string? Organization { get; set; }
        public int OperationUnitId { get; set; }
        public string? OperationUnit { get; set; }
        public int ParentOpunitId { get; set; }
        public string? ParentOpunit { get; set; }
        public int ManagerId { get; set; }
        public string? Manager { get; set; }
    }
}
