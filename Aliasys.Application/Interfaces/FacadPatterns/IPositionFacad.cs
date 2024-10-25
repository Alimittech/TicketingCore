using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.PositionServices.Commands.CreatePosition;
using Aliasys.Application.Services.InternalServices.PositionServices.Commands.UpdatePosition;
using Aliasys.Application.Services.InternalServices.PositionServices.Queries.FindPosition;
using Aliasys.Application.Services.InternalServices.PositionServices.Queries.GetPosition.GetPositionFullList;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aliasys.Application.Interfaces.FacadPatterns
{
    public interface IPositionFacad
    {
        IFindService<ResultFindPositionDto, int> FindPositionWithIdService {  get; }
        IFindService<ResultFindPositionDto, string> FindPositionWithNameService {  get; }
        IGetService<ResultGetPositionFullListDto, PaginationDto> GetPositionFullListService { get; }
        IGetService<List<SelectListItem>> GetPositionSelectListService { get; }
        ICreateService<int?, RequestCreatePositionDto> CreatePositionService { get; }
        IUpdateService<int?, RequestUpdatePositionDto> UpdatePositionService { get; }
        IDeleteService DeletePositionService { get; }
    }
}
