using Aliasys.Application.Services.InternalServices.UserServices.Commands.UpdateUser;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.User
{
    public class UpdateUserViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(RequestUpdateUserDto request)
        {
            return View("UpdateUserViewComponent", request);
        }
    }
}
