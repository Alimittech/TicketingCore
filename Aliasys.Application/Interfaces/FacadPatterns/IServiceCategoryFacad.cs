using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceCategoryServices.Commands.CreateServiceCategory;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceCategoryServices.Commands.UpdateServiceCategory;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceCategoryServices.Queries.FindServiceCategory;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceCategoryServices.Queries.GetServiceCategory.GetServiceCatFullList;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aliasys.Application.Interfaces.FacadPatterns
{
    public interface IServiceCategoryFacad
    {
        IFindService<ResultFindServiceCategoryDto, int> FindServiceCategoryWithIdService {  get; }
        IFindService<ResultFindServiceCategoryDto, string> FindServiceCategoryWithNameService {  get; }
        IGetService<ResultGetServiceCategoryFullListDto, PaginationDto> GetServiceCategoryFullListService { get; }
        IGetService<List<SelectListItem>> GetServiceCategorySelectListService { get; }
        ICreateService<int?, RequestCreateServiceCategoryDto> CreateServiceCategoryService { get; }
        IUpdateService<int?, RequestUpdateServiceCategoryDto> UpdateServiceCategoryService { get; }
        IDeleteService DeleteServiceCategoryService { get; }
    }
}
