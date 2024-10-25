using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.OperationUnit
{
    public class CreateOperationUnitViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(viewName: "CreateOperationUnitViewComponent");
        }
    }
}
