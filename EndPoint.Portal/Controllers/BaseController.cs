using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EndPoint.Portal.Controllers
{

    public class BaseController : Controller
    {
      
        //--------- Session Management -----------
        protected void SetSession(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
        }

        protected string? GetSession(string key)
        {
            return HttpContext.Session.GetString(key);
        }

        //--------- TempData Management -----------
        protected void SetTempData(string name, bool keepData, TempDataUserDto tempData)
        {
            TempData[name] = JsonConvert.SerializeObject(tempData);
            if (keepData)
            {
                TempData.Keep();
            }
        }

        protected TempDataUserDto GetTempData(string name, bool keepData)
        {
            TempDataUserDto tempData = JsonConvert.DeserializeObject<TempDataUserDto>(TempData[name].ToString());
            if (keepData)
            {
                TempData.Keep();
            }
            return tempData;
        }

        protected void RemoveTempData(string name)
        {
            TempData.Remove(name);
        }

        //--------- Cookie Management -----------
        protected void SetCookie(string key, string vlaue)
        {
            HttpContext.Response.Cookies.Append(key, vlaue);
        }

        protected string? GetCookie(string key)
        {
            if (HttpContext.Request.Cookies[key] != null)
            {
                return HttpContext.Request.Cookies[key];
            }
            return null;
        }

        //--------- Send Notify - use for sweetalert2 -----------
        public void Notify(string title, string message, NotifyType notifyType, string confirmButtonText, bool showCancelButton)
        {
            var msg = new
            {
                title = title,
                message = message,
                icon = notifyType.ToString(),
                confirmButtonText = confirmButtonText,
                showCancelButton = showCancelButton
            };
            var test = JsonConvert.SerializeObject(msg);
            ViewData["Message"] = test;
        }
    }

    public enum NotifyType
    {
        info = 0,
        warning = 1,
        error = 2,
        success = 3,
        question = 4,
    }
}
