using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestTypeServices.Commands.UpdateServiceRequestType;
using Aliasys.Domain.Entities.ServiceEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.ServiceRequestType
{
    public class UpdateServiceRequestTypeViewComponent : ViewComponent
    {
        private readonly IServiceCategoryFacad _serviceCategoryFacad;
        public UpdateServiceRequestTypeViewComponent(IServiceCategoryFacad serviceCategoryFacad)
        {
            _serviceCategoryFacad = serviceCategoryFacad;
        }

        public IViewComponentResult Invoke(RequestUpdateServiceRequestTypeDto request)
        {
            var requestTypeList = Enum.GetValues(typeof(RequestType)).Cast<RequestType>()
                        .Select(p => new SelectListItem
                        {
                            Text = p.ToString(),
                            Value = ((int)p).ToString()
                        });
            if (requestTypeList != null)
            {
                int reqTypeValue = (int)Enum.Parse(typeof(RequestType), request.RequestType.ToString());
                ViewBag.RequestTypeList = new SelectList(requestTypeList, "Value", "Text", reqTypeValue);
            }
            return View("UpdateServiceRequestTypeViewComponent", request);
        }
    }
}
