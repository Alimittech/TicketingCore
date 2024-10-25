using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.Organization
{
    public class CreateOrganizationViewComponent : ViewComponent
    {
        private readonly IRegionFacad _regionFacad;
        private readonly IOrganizationFacad _organizationFacad;

        public CreateOrganizationViewComponent(IRegionFacad regionFacad, IOrganizationFacad organizationFacad)
        {
            _regionFacad = regionFacad;
            _organizationFacad = organizationFacad;
        }
        public IViewComponentResult Invoke()
        {
            var regList = _regionFacad.GetRegionSelectListService.Get().Data;
            if (regList != null)
            {
                ViewBag.RegionList = new SelectList(regList, "Value", "Text", 0);
            }
            var orgList = _organizationFacad.GetOrganizationSelectListService.Get();
            if (orgList.ActionType == ActionType.Null)
            {
                TempData["OrganizationListCheck"] = false;
            }
            else if (orgList.ActionType == ActionType.Completed)
            {
                TempData["OrganizationListCheck"] = true;
                ViewBag.OrganizationList = new SelectList(orgList.Data, "Value", "Text", 0);
            }
            return View(viewName: "CreateOrganizationViewComponent");
        }
    }
}
