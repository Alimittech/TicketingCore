using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.ServiceState
{
    public class DataTableServiceStateViewComponent : ViewComponent
    {
        private readonly IServiceStateFacad _serviceStateFacad;
        public DataTableServiceStateViewComponent(IServiceStateFacad serviceStateFacad)
        {
            _serviceStateFacad = serviceStateFacad;
        }

        public IViewComponentResult Invoke(string searchKey, int page, int pageSize)
        {
            var serviceStateList = _serviceStateFacad.GetServiceStateFullListService.Get(new RequestDto<PaginationDto>
            {
                Parameter = new PaginationDto { SearchKey = searchKey, Page = page, PageSize = pageSize }
            });
            return View("~/Areas/Portal/Views/ServiceState/Components/DataTableServiceState/DataTableServiceStateViewComponent.cshtml", serviceStateList.Data);
        }
    }
}
