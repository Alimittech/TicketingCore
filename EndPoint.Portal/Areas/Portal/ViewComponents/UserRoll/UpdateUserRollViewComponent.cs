using Aliasys.Application.Services.InternalServices.UserRollServices.Commands.UpdateUserRoll;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.Roll
{
    public class UpdateUserRollViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(RequestUpdateUserRollDto request)
        {
            return View("UpdateUserRollViewComponent", request);
        }
    }
}
