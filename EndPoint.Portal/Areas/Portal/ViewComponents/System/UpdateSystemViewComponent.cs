using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Services.InternalServices.SystemServices.Commands.UpdateSystem;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.System
{
    public class UpdateSystemViewComponent : ViewComponent
    {
        private readonly ISystemFacad _systemFacad;
        public UpdateSystemViewComponent(ISystemFacad systemFacad)
        {
            _systemFacad = systemFacad;
        }

        public IViewComponentResult Invoke(RequestUpdateSystemDto request)
        {
            var getParentSystem = _systemFacad.FindSystemWithIdService.Find(new RequestDto<int>
            {
                Parameter = request.Id
            });
            var sysList = _systemFacad.GetSystemSelectListService.Get();
            if (sysList.ActionType == ActionType.Null)
            {
                TempData["SystemListCheck"] = false;
            }
            else if (sysList.ActionType == ActionType.Completed)
            {
                TempData["SystemListCheck"] = true;
                ViewBag.SystemList = new SelectList(sysList.Data, "Value", "Text", getParentSystem.Data.ParentSystem.Value);
            }
            return View(viewName: "UpdateSystemViewComponent", request);
        }
    }
}
