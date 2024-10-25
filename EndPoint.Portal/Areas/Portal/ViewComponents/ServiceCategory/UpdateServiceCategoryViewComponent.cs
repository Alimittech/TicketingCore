using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceCategoryServices.Commands.UpdateServiceCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.ServiceCategory
{
    public class UpdateServiceCategoryViewComponent : ViewComponent
    {
        private readonly IUserGroupFacad _userGroupFacad;
        public UpdateServiceCategoryViewComponent(IUserGroupFacad userGroupFacad)
        {
            _userGroupFacad = userGroupFacad;
        }
        public IViewComponentResult Invoke(RequestUpdateServiceCategoryDto request)
        {
            var groupList = _userGroupFacad.GetUserGroupSelectListService.Get().Data;
            if (groupList != null)
            {
                ViewBag.GroupList = new SelectList(groupList, "Value", "Text", request.UserGroupId_FK);
            }
            return View("UpdateServiceCategoryViewComponent", request);
        }
    }
}
