namespace Aliasys.Application.Services.InternalServices.SystemServices.Queries.GetSystem.GetSystemFullList
{
    public class RequestGetSystemFullListDto
    {
        public int Id { get; set; }
        public string ParentSystem { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
