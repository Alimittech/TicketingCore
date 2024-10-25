namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServicePhaseService.Queries.FindServicePhase
{
    public class ResultFindServicePhaseDto
    {
        public int Id { get; set; }
        public int ServiceRequestTypeId_FK { get; set; }
        public string PhaseName { get; set; }
    }
}
