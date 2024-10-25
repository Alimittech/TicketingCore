using Aliasys.Common.Constants;
using EndPoint.Portal.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Error.Controllers
{
    [Area("Error")]
    public class ErrorHandlingController : BaseController
    {
        public IActionResult Error(int errorCode = 404)
        {
            //HttpStatusCode httpStatusCode = HttpStatusCode.NotFound;
            ViewBag.Title = $"Error {errorCode}";
            switch (errorCode)
            {
                case 404:
                    ViewBag.Message = Messages.ShowMessages(MessageTitleType.Url_NotFound).Message;
                    //httpStatusCode = HttpStatusCode.NotFound;
                    return View();
                case 440:
                    ViewBag.Message = Messages.ShowMessages(MessageTitleType.Url_NotFound).Message;
                    //httpStatusCode = HttpStatusCode.Unauthorized;
                    return View();
            }
            return View();
        }
    }
}
