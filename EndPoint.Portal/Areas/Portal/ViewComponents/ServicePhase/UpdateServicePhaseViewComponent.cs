using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceStateServices.Commands.UpdateServiceState;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.ServicePhase
{
    public class UpdateServicePhaseViewComponent : ViewComponent
    {
        private readonly IServiceRequestTypeFacad _serviceRequestTypeFacad;
        public UpdateServicePhaseViewComponent(IServiceRequestTypeFacad serviceRequestTypeFacad)
        {
            _serviceRequestTypeFacad = serviceRequestTypeFacad;
        }

        public IViewComponentResult Invoke(RequestUpdateServiceStateDto request)
        {
            var serviceReqTypeList = _serviceRequestTypeFacad.GetServiceRequestTypeSelectListService.Get().Data;
            if (serviceReqTypeList != null)
            {
                ViewBag.ServiceReqTypeList = new SelectList(serviceReqTypeList, "Value", "Text", request.ServiceRequestTypeId_FK);
            }
            return View("UpdateServicePhaseViewComponent", request);
        }
    }
}
