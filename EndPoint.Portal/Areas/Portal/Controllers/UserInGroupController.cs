using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Services.InternalServices.PositionServices.Commands.CreatePosition;
using Aliasys.Application.Services.InternalServices.UserInGroupServices.Commands.CreateUserInGroup;
using Aliasys.Common.Dtos;
using Aliasys.Common.ExtentionMethods;
using Aliasys.Common.Paginations;
using EndPoint.Portal.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.Controllers
{
    [Area(nameof(Portal))]
    [Authorize]
    public class UserInGroupController : BaseController
    {
        private readonly IUserInGroupFacad _userInGroupFacad;
        public UserInGroupController(IUserInGroupFacad userInGroupFacad)
        {
            _userInGroupFacad = userInGroupFacad;
        }

        #region Create Action
        [HttpGet]
        public IActionResult Create(string httpVerb)
        {
            if (httpVerb == "GET")
            {
                if (CustomExtentionMethods.IsAjaxRequest(HttpContext.Request, httpVerb).IsSuccess)
                {
                    return ViewComponent("CreateUserInGroup");
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Create(RequestCreateUserInGroupDto request)
        {
            return View(request);
        }
        #endregion
    }
}
