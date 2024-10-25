using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Domain.Entities.ServiceEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.ServiceRequestType
{
    public class CreateServiceRequestTypeViewComponent : ViewComponent
    {
        private readonly IServiceCategoryFacad _serviceCategoryFacad;
        public CreateServiceRequestTypeViewComponent(IServiceCategoryFacad serviceCategoryFacad)
        {
            _serviceCategoryFacad = serviceCategoryFacad;
        }
        public IViewComponentResult Invoke()
        {
            var requestTypeList = Enum.GetValues(typeof(RequestType)).Cast<RequestType>()
                        .Select(p => new SelectListItem
                        {
                            Text = p.ToString(),
                            Value = ((int)p).ToString()
                        });
            if (requestTypeList != null)
            {
                ViewBag.RequestTypeList = new SelectList(requestTypeList, "Value", "Text", 0);
            }
            return View("CreateServiceRequestTypeViewComponent");
        }
    }
}
