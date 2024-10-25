using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.Position
{
    public class CreatePositionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("CreatePositionViewComponent");
        }
    }
}
