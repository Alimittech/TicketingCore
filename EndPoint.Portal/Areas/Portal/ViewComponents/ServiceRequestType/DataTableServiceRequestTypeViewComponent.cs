using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.ServiceRequestType
{
    public class DataTableServiceRequestTypeViewComponent : ViewComponent
    {
        private readonly IServiceRequestTypeFacad _serviceRequestTypeFacad;
        public DataTableServiceRequestTypeViewComponent(IServiceRequestTypeFacad serviceRequestTypeFacad)
        {
            _serviceRequestTypeFacad = serviceRequestTypeFacad;
        }
        public IViewComponentResult Invoke(string searchKey, int page, int pageSize)
        {
            var serviceReqTypeList = _serviceRequestTypeFacad.GetServiceRequestTypeFullListService.Get(new RequestDto<PaginationDto>
            {
                Parameter = new PaginationDto { SearchKey = searchKey, Page = page, PageSize = pageSize }
            });
            return View("~/Areas/Portal/Views/ServiceRequestType/Components/DataTableServiceRequestType/DataTableServiceRequestTypeViewComponent.cshtml", serviceReqTypeList.Data);
        }
    }
}
