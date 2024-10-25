namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceStateServices.Queries.GetServiceState
{
    public class RequestGetServiceStateFullListDto
    {
        public int Id { get; set; }
        public string ServiceRequestType { get; set; }
        public string StateName { get; set; }
    }
}
