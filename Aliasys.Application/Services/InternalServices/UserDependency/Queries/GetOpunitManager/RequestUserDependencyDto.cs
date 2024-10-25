namespace Aliasys.Application.Services.InternalServices.UserDependency.Queries.GetOpunitManager
{
    public class RequestUserDependencyDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OrganizationId { get; set; }
        public int OperationUnitId { get; set; }
        public int PositionId { get; set; }
    }
}
