namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServicePhaseService.Commands.UpdateServicePhase
{
    public class RequestUpdateServicePhaseDto
    {
        public int Id { get; set; }
        public int ServiceRequestTypeId_FK { get; set; }
        public string PhaseName { get; set; }
    }
}
