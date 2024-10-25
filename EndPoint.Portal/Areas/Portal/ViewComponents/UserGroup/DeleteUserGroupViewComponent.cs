using Aliasys.Application.Services.InternalServices.UserGroupServices.Commands.UpdateUserGroup;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.UserGroup
{
    public class DeleteUserGroupViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(RequestUpdateUserGroupDto request)
        {
            return View("UpdateUserGroupViewComponent", request);
        }
    }
}
