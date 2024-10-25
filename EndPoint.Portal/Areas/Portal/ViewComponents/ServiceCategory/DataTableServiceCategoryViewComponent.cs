using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.ServiceCategory
{
    public class DataTableServiceCategoryViewComponent : ViewComponent
    {
        private readonly IServiceCategoryFacad _serviceCategoryFacad;
        public DataTableServiceCategoryViewComponent(IServiceCategoryFacad serviceCategoryFacad)
        {
            _serviceCategoryFacad = serviceCategoryFacad;
        }
        public IViewComponentResult Invoke(string searchKey, int page, int pageSize)
        {
            var serviceCatList = _serviceCategoryFacad.GetServiceCategoryFullListService.Get(new RequestDto<PaginationDto>
            {
                Parameter = new PaginationDto { SearchKey = searchKey, Page = page, PageSize = pageSize }
            });
            return View("DataTableServiceCategoryViewComponent", serviceCatList.Data);
        }
    }
}
