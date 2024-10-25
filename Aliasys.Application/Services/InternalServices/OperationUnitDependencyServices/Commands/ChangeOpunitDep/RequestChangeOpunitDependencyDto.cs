namespace Aliasys.Application.Services.InternalServices.OperationUnitDependencyServices.Commands.ChangeOpunitDep
{
    public class RequestChangeOpunitDependencyDto
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int OperationUnitId { get; set; }
        public int ParentOperationUnitId { get; set; }
        public int ManagerId { get; set; }
    }
}
