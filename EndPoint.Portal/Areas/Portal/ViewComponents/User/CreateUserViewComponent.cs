using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.User
{
    public class CreateUserViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("CreateUserViewComponent");
        }
    }
}
