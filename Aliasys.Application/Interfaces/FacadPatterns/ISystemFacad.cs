using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.SystemServices.Commands.CreateSystem;
using Aliasys.Application.Services.InternalServices.SystemServices.Commands.UpdateSystem;
using Aliasys.Application.Services.InternalServices.SystemServices.Queries.FindSystem;
using Aliasys.Application.Services.InternalServices.SystemServices.Queries.GetSystem.GetSystemAll;
using Aliasys.Application.Services.InternalServices.SystemServices.Queries.GetSystem.GetSystemFullList;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aliasys.Application.Interfaces.FacadPatterns
{
    public interface ISystemFacad
    {
        IFindService<ResultFindSystemDto, int> FindSystemWithIdService {  get; }
        IFindService<ResultFindSystemDto, string> FindSystemWithNameService {  get; }
        IGetService<List<SelectListItem>> GetSystemSelectListService {  get; }
        IGetService<ResultGetSystemFullListDto, PaginationDto> GetSystemFullListService { get; }
        IGetService<List<ResultGetSystemAllDto>> GetSystemAllService { get; }
        ICreateService<int? , RequestCreateSystemDto> CreateSystemService { get; }
        IUpdateService<int? , RequestUpdateSystemDto> UpdateSystemService { get; }
        IDeleteService DeleteSystemService { get; }
    }
}
