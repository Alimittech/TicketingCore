using Aliasys.Application.Services.InternalServices.PositionServices.Commands.UpdatePosition;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.Position
{
    public class UpdatePositionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(RequestUpdatePositionDto request)
        {
            return View("UpdatePositionViewComponent", request);
        }
    }
}
