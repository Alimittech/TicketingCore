using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Domain.Entities.ServiceEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.ServiceRequest
{
    public class CreateServiceRequestViewComponent : ViewComponent
    {
        private readonly IServiceCategoryFacad _serviceCategoryFacad;
        private readonly IServiceRequestTypeFacad _serviceRequestTypeFacad;
        public CreateServiceRequestViewComponent(IServiceCategoryFacad serviceCategoryFacad,
                                    IServiceRequestTypeFacad serviceRequestTypeFacad)
        {
            _serviceCategoryFacad = serviceCategoryFacad;
            _serviceRequestTypeFacad = serviceRequestTypeFacad;
        }

        public IViewComponentResult Invoke()
        {
            var serviceCatList = _serviceCategoryFacad.GetServiceCategorySelectListService.Get().Data;
            if (serviceCatList != null)
            {
                ViewBag.ServiceCategoryList = new SelectList(serviceCatList, "Value", "Text", 0);
            }

            var ServiceReqList = _serviceRequestTypeFacad.GetServiceRequestTypeSelectListService.Get().Data;
            if (ServiceReqList != null)
            {
                ViewBag.ServiceRequestTypeList = new SelectList(ServiceReqList, "Value", "Text", 0);
            } 

            ViewBag.ServicePriorityList = Enum.GetValues(typeof(ServicePriority)).Cast<ServicePriority>()
                        .Select(p => new SelectListItem
                        {
                            Text = p.ToString(),
                            Value = ((int)p).ToString()
                        });

            ViewBag.ImpactOn = Enum.GetValues(typeof(ImpactOn)).Cast<ImpactOn>()
                        .Select(p => new SelectListItem
                        {
                            Text = p.ToString(),
                            Value = ((int)p).ToString()
                        });

            return View("CreateServiceRequestViewComponent");
        }
    }
}
