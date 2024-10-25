using Aliasys.Application.Services.InternalServices.OperationUnitServices.Commands.UpdateOperationUnit;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.OperationUnit
{
    public class UpdateOperationUnitViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(RequestUpdateOperationUnitDto request)
        {
            return View("UpdateOperationUnitViewComponent", request);
        }
    }
}
