namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServicePhaseService.Commands.CreateServicePhase
{
    public class RequestCreateServicePhaseDto
    {
        public int ServiceRequestTypeId_FK { get; set; }
        public string PhaseName { get; set; }
    }
}
