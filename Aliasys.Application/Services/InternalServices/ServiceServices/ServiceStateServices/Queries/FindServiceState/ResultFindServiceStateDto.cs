namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceStateServices.Queries.FindServiceState
{
    public class ResultFindServiceStateDto
    {
        public int Id { get; set; }
        public int ServiceRequestTypeId_FK { get; set; }
        public string StateName { get; set; }
    }
}
