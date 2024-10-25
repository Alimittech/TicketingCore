using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.System
{
    public class CreateSystemViewComponent : ViewComponent
    {
        private readonly ISystemFacad _systemFacad;
        public CreateSystemViewComponent(ISystemFacad systemFacad)
        {
            _systemFacad = systemFacad;
        }

        public IViewComponentResult Invoke()
        {
            var sysList = _systemFacad.GetSystemSelectListService.Get();
            if (sysList.ActionType == ActionType.Null)
            {
                TempData["SystemListCheck"] = false;
            }
            else if (sysList.ActionType == ActionType.Completed)
            {
                TempData["SystemListCheck"] = true;
                ViewBag.SystemList = new SelectList(sysList.Data, "Value", "Text", 0);
            }
            return View(viewName: "CreateSystemViewComponent");
        }
    }
}
