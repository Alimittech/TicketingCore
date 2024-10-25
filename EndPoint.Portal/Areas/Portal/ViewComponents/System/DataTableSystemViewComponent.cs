using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.System
{
    public class DataTableSystemViewComponent : ViewComponent
    {
        private readonly ISystemFacad _systemFacad;

        public DataTableSystemViewComponent(ISystemFacad systemFacad)
        {
            _systemFacad = systemFacad;
        }
        public IViewComponentResult Invoke(string searchKey, int page, int pageSize)
        {
            var systemList = _systemFacad.GetSystemFullListService.Get(new RequestDto<PaginationDto>
            {
                Parameter = new PaginationDto
                {
                    SearchKey = searchKey,
                    Page = page,
                    PageSize = pageSize
                }
            });
            return View(viewName: "DataTableSystemViewComponent", model: systemList.Data);
        }
    }
}
