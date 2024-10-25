using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.UserGroup
{
    public class CreateUserGroupViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("CreateUserGroupViewComponent");
        }
    }
}
