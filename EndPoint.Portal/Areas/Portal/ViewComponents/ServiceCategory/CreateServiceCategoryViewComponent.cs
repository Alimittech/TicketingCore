using Aliasys.Application.Interfaces.FacadPatterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.ServiceCategory
{
    public class CreateServiceCategoryViewComponent : ViewComponent
    {
        private readonly IUserGroupFacad _userGroupFacad;
        public CreateServiceCategoryViewComponent(IUserGroupFacad userGroupFacad)
        {
            _userGroupFacad = userGroupFacad;
        }
        public IViewComponentResult Invoke()
        {
            var groupList = _userGroupFacad.GetUserGroupSelectListService.Get().Data;
            if (groupList != null)
            {
                ViewBag.GroupList = new SelectList(groupList, "Value", "Text", 0);
            }
            return View(viewName: "CreateServiceCategoryViewComponent");
        }
    }
}
