using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Services.InternalServices.OrganizationServices.Commands.UpdateOrganization;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.Organization
{
    public class UpdateOrganizationViewComponent : ViewComponent
    {
        private readonly IRegionFacad _regionFacad;
        private readonly IOrganizationFacad _organizationFacad;

        public UpdateOrganizationViewComponent(IRegionFacad regionFacad, IOrganizationFacad organizationFacad)
        {
            _regionFacad = regionFacad;
            _organizationFacad = organizationFacad;
        }

        public IViewComponentResult Invoke(RequestUpdateOrganizationDto request)
        {
            var getOrgDependency = _organizationFacad.GetOrganizationDependencyService.Get(new RequestDto<int>
            {
                Parameter = request.Id,
            });

            var regList = _regionFacad.GetRegionSelectListService.Get().Data;
            if (regList != null)
            {
                ViewBag.RegionList = new SelectList(regList, "Value", "Text", getOrgDependency.Data.RegionId);
            }

            var orgList = _organizationFacad.GetOrganizationSelectListService.Get();
            if (orgList.ActionType == ActionType.Null || request.Id == 1)
            {
                TempData["OrganizationListCheck"] = false;
            }
            else if (orgList.ActionType == ActionType.Completed)
            {
                TempData["OrganizationListCheck"] = true;
                ViewBag.DepartmentList = new SelectList(orgList.Data, "Value", "Text", getOrgDependency.Data.ParentOrganizationId);
            }

            return View(viewName: "UpdateOrganizationViewComponent", request);
        }
    }
}
