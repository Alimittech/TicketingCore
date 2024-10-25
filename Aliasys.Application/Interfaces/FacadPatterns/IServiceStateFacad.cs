using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceStateServices.Commands.CreateServiceState;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceStateServices.Commands.UpdateServiceState;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceStateServices.Queries.FindServiceState;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceStateServices.Queries.GetServiceState;
using Aliasys.Common.Dtos;

namespace Aliasys.Application.Interfaces.FacadPatterns
{
    public interface IServiceStateFacad
    {
        IFindService<ResultFindServiceStateDto, int> FindServiceStateWithIdService {  get; }
        IFindService<ResultFindServiceStateDto, string> FindServiceStateWithNameService {  get; }
        IGetService<ResultGetServiceStateFullListDto, PaginationDto> GetServiceStateFullListService { get; }
        IGetService<List<ResultGetServiceStateAllDto>> GetServiceStateAllService { get; }
        ICreateService<int?, RequestCreateServiceStateDto> CreateServiceStateService { get; }
        IUpdateService<int?, RequestUpdateServiceStateDto> UpdateServiceStateService { get; }
        IDeleteService DeleteServiceStateService { get; }
    }
}
