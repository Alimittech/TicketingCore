using Aliasys.Common.ExtentionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.Controllers
{
    [Area(nameof(Portal))]
    [Authorize]
    public class UserDependencyController : Controller
    {
        //---------- Create Action ----------
        #region Create Action
        [HttpGet]
        public IActionResult Create(string httpVerb)
        {
            if (httpVerb == "GET")
            {
                if (CustomExtentionMethods.IsAjaxRequest(HttpContext.Request, httpVerb).IsSuccess)
                {
                    return ViewComponent("CreateUserDependency");
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Create()
        {
            return View();
        }
        #endregion
    }
}
