using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Services.InternalServices.UserServices.Queries.GetUser.ValidateLdapUser;
using Aliasys.Common.Dtos;
using EndPoint.Portal.Areas.Authentication.Models.ViewModels;
using EndPoint.Portal.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace EndPoint.Portal.Areas.Authentication.Controllers
{
   
    [Area("Authentication")]
    public class AccountController : BaseController
    {
        private readonly IOrganizationFacad _organizationFacad;
        private readonly IUserFacad _userFacad;
        private readonly IUserDependencyFacad _userDependencyFacad;
        private readonly IUserInRollFacad _userInRollFacad;
        private readonly IUserInGroupFacad _userInGroupFacad;
        public AccountController(IOrganizationFacad organizationFacad,
                                 IUserFacad userFacad,
                                 IUserDependencyFacad userDependencyFacad,
                                 IUserInRollFacad userInRollFacad,
                                 IUserInGroupFacad userInGroupFacad)
        {
            _organizationFacad = organizationFacad;
            _userFacad = userFacad;
            _userDependencyFacad = userDependencyFacad;
            _userInRollFacad = userInRollFacad;
            _userInGroupFacad = userInGroupFacad;
        }

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            var findOrg = _organizationFacad.FindOrganizationWithNameService.Find(new RequestDto<string>
            {
                Parameter = "Aliasys Company"
            });
            LoginViewModel model = new LoginViewModel();
            if (findOrg != null)
            {
                model.DomainName = findOrg.Data.DomainName;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel request)
        {
            if (ModelState.IsValid)
            {
                //check active user
                var checkUserActive = _userFacad.FindUserWithUserNameService.Find(new RequestDto<string>
                {
                    Parameter = request.UserName
                });
                if (checkUserActive.IsSuccess)
                {
                    if (checkUserActive.Data.IsActive)
                    {
                        var validateUser = _userFacad.ValidateLdapUser.ValidateUser(new RequestValidateLdapUserDto
                        {
                            UserName = request.UserName,
                            Password = request.Password,
                            DomainName = request.DomainName
                        });
                        if (validateUser.IsSuccess)
                        {
                            var userDep = _userFacad.GetUserDetailWithUserNameService.Get(new RequestDto<string>
                            {
                                Parameter = request.UserName,
                            });
                            if (userDep.IsSuccess)
                            {
                                var userGroups = _userInGroupFacad.GetUserInGroupWithUserIdService.Get(new RequestDto<int>
                                {
                                    Parameter = userDep.Data.Id
                                });
                                TempDataUserDto UserInfo = new TempDataUserDto()
                                {
                                    UserId = userDep.Data.Id,
                                    UserName = userDep.Data.UserName,
                                    DisplayName = userDep.Data.DisplayName,
                                    Email = userDep.Data.Email,
                                    PhoneNumber = userDep.Data.PhoneNumber,
                                    ExtentionNumber = userDep.Data.ExtentionNumber,
                                    PersonCode = userDep.Data.PersonCode,
                                    IsActive = userDep.Data.IsActive,
                                    Organiztion = userDep.Data.Organization,
                                    OperationUnit = userDep.Data.OperationUnit,
                                    Position = userDep.Data.Position,
                                    Manager = userDep.Data.ManagerName,
                                    Roll = userDep.Data.UserRollName,
                                    UserGroups = userGroups.Data,
                                    ImageName = userDep.Data.ImageName
                                };
                                TempData["UserInfo"] = JsonConvert.SerializeObject(UserInfo);
                                TempData.Keep();
                                var claim = new List<Claim>()
                                    {
                                        new Claim(ClaimTypes.NameIdentifier, UserInfo.UserName),
                                        new Claim(ClaimTypes.Name, UserInfo.DisplayName),
                                        new Claim(ClaimTypes.Email, UserInfo.Email),
                                        new Claim(ClaimTypes.MobilePhone, UserInfo.PhoneNumber),
                                        new Claim(ClaimTypes.OtherPhone, UserInfo.ExtentionNumber)
                                    };
                                var identity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
                                var principal = new ClaimsPrincipal(identity);
                                HttpContext.SignInAsync(principal);
                                return RedirectPermanent("/portal");
                            }
                            ModelState.AddModelError("DomainName", "Username or password is not valid!");
                            return View(nameof(Login), request);
                        }
                        ModelState.AddModelError("DomainName", "Username or password is not correct!");
                        return View(nameof(Login), request);
                    }
                    ModelState.AddModelError("DomainName", "Access is denied!");
                    return View(nameof(Login), request);
                }
                ModelState.AddModelError("DomainName", "Username or password is invalid!");
                return View(nameof(Login), request);
            }
            return View();
        }
        #endregion

        #region Logout
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectPermanent("/");
        }
        #endregion
    }
}
