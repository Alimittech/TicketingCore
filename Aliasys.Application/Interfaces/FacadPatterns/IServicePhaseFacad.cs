using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServicePhaseService.Commands.CreateServicePhase;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServicePhaseService.Commands.UpdateServicePhase;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServicePhaseService.Queries.FindServicePhase;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServicePhaseService.Queries.GetServicePhase;
using Aliasys.Common.Dtos;

namespace Aliasys.Application.Interfaces.FacadPatterns
{
    public interface IServicePhaseFacad
    {
        IFindService<ResultFindServicePhaseDto, int> FindServicePhaseWithIdService { get; }
        IFindService<ResultFindServicePhaseDto, string> FindServicePhaseWithNameService { get; }
        IGetService<ResultGetServicePhaseFullListDto, PaginationDto> GetServicePhaseFullListService { get; }
        IGetService<List<ResultGetServicePhaseAllDto>> GetServicePhaseAllService { get; }
        ICreateService<int?, RequestCreateServicePhaseDto> CreateServicePhaseService { get; }
        IUpdateService<int?, RequestUpdateServicePhaseDto> UpdateServicePhaseService { get; }
        IDeleteService DeleteServicePhaseService { get; }
    }
}
