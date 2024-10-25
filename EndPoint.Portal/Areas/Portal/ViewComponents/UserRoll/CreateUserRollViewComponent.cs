using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.Roll
{
    public class CreateUserRollViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("CreateUserRollViewComponent");
        }
    }
}
