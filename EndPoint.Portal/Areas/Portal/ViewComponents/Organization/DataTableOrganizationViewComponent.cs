using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.Organization
{
    public class DataTableOrganizationViewComponent : ViewComponent
    {
        private readonly IOrganizationFacad _organizationFacad;
        public DataTableOrganizationViewComponent(IOrganizationFacad organizationFacad)
        {
            _organizationFacad = organizationFacad;
        }
        public IViewComponentResult Invoke(string searchKey, int page, int pageSize)
        {
            var orgList = _organizationFacad.GetOrganizationFullListService.Get(new RequestDto<PaginationDto>
            {
                Parameter = new PaginationDto { SearchKey = searchKey, Page = page, PageSize = pageSize }
            });
            return View(viewName: "DataTableOrganizationViewComponent", model: orgList.Data);
        }
    }
}