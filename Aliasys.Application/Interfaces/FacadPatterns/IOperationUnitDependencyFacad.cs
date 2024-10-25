using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.OperationUnitDependencyServices.Commands.ChangeOpunitDep;
using Aliasys.Application.Services.InternalServices.OperationUnitDependencyServices.Queries.FindOpunitDep;

namespace Aliasys.Application.Interfaces.FacadPatterns
{
    public interface IOperationUnitDependencyFacad
    {
        IFindService<ResultFindOpunitDepDto> FindOpunitDepWithManagerIdService { get; }
        IFindService<ResultFindOpunitDepDto, int> FindOpunitDepWithOpunitIdService { get; }
        IChangeService<RequestChangeOpunitDependencyDto> ChangeOpunitDependencyService { get; }
    }
}
