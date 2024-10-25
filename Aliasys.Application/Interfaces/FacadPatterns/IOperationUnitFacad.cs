using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.OperationUnitServices.Commands.CreateOperationUnit;
using Aliasys.Application.Services.InternalServices.OperationUnitServices.Commands.UpdateOperationUnit;
using Aliasys.Application.Services.InternalServices.OperationUnitServices.Queries.FindOperationUnit;
using Aliasys.Application.Services.InternalServices.OperationUnitServices.Queries.GetOperationUnit.GetOpUnitFullList;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aliasys.Application.Interfaces.FacadPatterns
{
    public interface IOperationUnitFacad
    {
        IFindService<ResultFindOperationUnitDto, int> FindOperationUnitWithIdService { get; }
        IFindService<ResultFindOperationUnitDto, string> FindOperationUnitWithNameService { get; }
        IFindService<ResultFindOperationUnitDto, int> FindOperationUnitWithCodeService { get; }
        IGetService<ResultGetOperationUnitFullListDto, PaginationDto> GetOperationUnitFullListService { get; }
        IGetService<List<SelectListItem>> GetOperationUnitSelectListService { get; }
        ICreateService<int?, RequestCreateOperationUnitDto> CreateOperationUnitService { get; }
        IUpdateService<int?, RequestUpdateOperationUnitDto> UpdateOperationUnitService { get; }
        IDeleteService DeleteOperationUnitService { get; }
    }
}
