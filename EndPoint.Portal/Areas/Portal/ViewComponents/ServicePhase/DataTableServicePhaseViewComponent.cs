using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.ServicePhase
{
    public class DataTableServicePhaseViewComponent : ViewComponent
    {
        private readonly IServicePhaseFacad _servicePhaseFacad;
        public DataTableServicePhaseViewComponent(IServicePhaseFacad servicePhaseFacad)
        {
            _servicePhaseFacad = servicePhaseFacad;
        }

        public IViewComponentResult Invoke(string searchKey, int page, int pageSize)
        {
            var servicePhaseList = _servicePhaseFacad.GetServicePhaseFullListService.Get(new RequestDto<PaginationDto>
            {
                Parameter = new PaginationDto { SearchKey = searchKey, Page = page, PageSize = pageSize }
            });
            return View("~/Areas/Portal/Views/ServicePhase/Components/DataTableServicePhase/DataTableServicePhaseViewComponent.cshtml", servicePhaseList.Data);
        }
    }
}
