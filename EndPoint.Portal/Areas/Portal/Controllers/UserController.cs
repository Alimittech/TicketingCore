using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Services.InternalServices.UserServices.Commands.CreateUser;
using Aliasys.Application.Services.InternalServices.UserServices.Commands.UpdateUser;
using Aliasys.Common.Dtos;
using Aliasys.Common.ExtentionMethods;
using Aliasys.Common.Paginations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.Controllers
{
    [Area(nameof(Portal))]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserFacad _userFacad;
        public UserController(IUserFacad userFacad)
        {
            _userFacad = userFacad;
        }

        //---------- List Action ----------
        #region List Action
        [HttpGet]
        public IActionResult List(string searchKey, int page, int pageSize)
        {
            var getUser = _userFacad.GetUserFullListService.Get(new RequestDto<PaginationDto>
            {
                Parameter = new PaginationDto
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchKey = searchKey
                }
            });
            if (getUser.ActionType == ActionType.Completed || getUser.ActionType == ActionType.Null)
            {
                string userListPath = "~/Areas/Portal/Views/User/Components/DataTableUser/DataTableUserViewComponent.cshtml";
                return Json(new
                {
                    isValid = true,
                    html = Helper.RenderRazorViewToString(this, userListPath, getUser.Data),
                    message = getUser.Message,
                    dataUpdate = "#resultDataTableUser"
                });
            }
            return Json(new { isValid = false, message = getUser.Message, errorMessage = true });
        }
        #endregion

        //---------- Create Action ----------
        #region Create Action
        [HttpGet]
        public IActionResult Create(string httpVerb)
        {
            if (httpVerb == "GET")
            {
                if (CustomExtentionMethods.IsAjaxRequest(HttpContext.Request, httpVerb).IsSuccess)
                {
                    return ViewComponent("CreateUser");
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Create(RequestCreateUserDto request)
        {
            if (ModelState.IsValid)
            {
                var createUser = _userFacad.CreateUserService.Create(new RequestDto<RequestCreateUserDto>
                {
                    Parameter = request
                });
                if (createUser.IsSuccess)
                {
                    var getUser = _userFacad.GetUserFullListService.Get(new RequestDto<PaginationDto>
                    {
                        Parameter = new PaginationDto
                        {
                            Page = 1,
                            PageSize = 5,
                            SearchKey = string.Empty
                        }
                    });
                    if (getUser.ActionType == ActionType.Completed || getUser.ActionType == ActionType.Null)
                    {
                        string userListPath = "~/Areas/Portal/Views/User/Components/DataTableUser/DataTableUserViewComponent.cshtml";
                        return Json(new
                        {
                            isValid = true,
                            html = Helper.RenderRazorViewToString(this, userListPath, getUser.Data),
                            message = createUser.Message,
                            dataUpdate = "#DataTableUser"
                        });
                    }
                }
                //ModelState.AddModelError(request.Name, "The entered name is already exist!");
                return Json(new { isValid = false, message = createUser.Message, errorMessage = true });
            }
            string UserCreatePath = "~/Areas/Portal/Views/User/Components/CreateUser/CreateUserViewComponent.cshtml";
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, UserCreatePath, request) });
        }
        #endregion

        //---------- Check Action ----------
        #region Check Action

        #endregion

        //---------- Update Action ----------
        #region Update Action
        [HttpGet]
        public IActionResult Update(string httpVerb, int Id)
        {
            if (httpVerb == "GET")
            {
                if (CustomExtentionMethods.IsAjaxRequest(HttpContext.Request, httpVerb).IsSuccess)
                {
                    var userFind = _userFacad.FindUserWithIdService.Find(new RequestDto<int>
                    {
                        Parameter = Id
                    });

                    if (userFind.IsSuccess)
                    {
                        var model = new RequestUpdateUserDto
                        {
                            Id = userFind.Data.Id,
                            UserName = userFind.Data.UserName,
                            DisplayName = userFind.Data.DisplayName,
                            Email = userFind.Data.Email,
                            PhoneNumber = userFind.Data.PhoneNumber,
                            ExtentionNumber = userFind.Data.ExtentionNumber,
                        };
                        return ViewComponent("UpdateUser", model);
                    }
                    return Json(new { isValid = false, errorMessage = true, message = userFind.Message });
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Update(RequestUpdateUserDto request)
        {
            if (ModelState.IsValid)
            {
                var updateUser = _userFacad.UpdateUserService.Update(new RequestDto<RequestUpdateUserDto>
                {
                    Parameter = request
                });
                if (updateUser.IsSuccess)
                {
                    var getUser = _userFacad.GetUserFullListService.Get(new RequestDto<PaginationDto>
                    {
                        Parameter = new PaginationDto
                        {
                            Page = 1,
                            PageSize = 5,
                            SearchKey = string.Empty
                        }
                    });
                    if (getUser.ActionType == ActionType.Completed || getUser.ActionType == ActionType.Null)
                    {
                        string userListPath = "~/Areas/Portal/Views/User/Components/DataTableUser/DataTableUserViewComponent.cshtml";
                        return Json(new
                        {
                            isValid = true,
                            html = Helper.RenderRazorViewToString(this, userListPath, getUser.Data),
                            message = updateUser.Message,
                            dataUpdate = "#DataTableUser"
                        });
                    }
                    return Json(new { isValid = false, message = getUser.Message, errorMessage = true });
                }
                return Json(new { isValid = false, message = updateUser.Message, errorMessage = true });
            }
            string userUpdatePath = "~/Areas/Portal/Views/User/Components/UpdateUser/UpdateUserViewComponent.cshtml";
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, userUpdatePath, request) });
        }
        #endregion

        //---------- Delete Action ----------
        #region Delete Action
        [HttpGet]
        public IActionResult Delete(string httpVerb, int Id)
        {
            if (httpVerb == "GET")
            {
                if (CustomExtentionMethods.IsAjaxRequest(HttpContext.Request, httpVerb).IsSuccess)
                {
                    ViewBag.RouteAction = "deleteUser";
                    return PartialView("_PartialDelete", Id);
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var user = _userFacad.DeleteUserService.Delete(Id);
            if (user.IsSuccess)
            {
                var getUser = _userFacad.GetUserFullListService.Get(new RequestDto<PaginationDto>
                {
                    Parameter = new PaginationDto
                    {
                        Page = 1,
                        PageSize = 5,
                        SearchKey = string.Empty
                    }
                });
                if (getUser.ActionType == ActionType.Completed || getUser.ActionType == ActionType.Null)
                {
                    string userListPath = "~/Areas/Portal/Views/User/Components/DataTableUser/DataTableUserViewComponent.cshtml";
                    return Json(new
                    {
                        isValid = true,
                        html = Helper.RenderRazorViewToString(this, userListPath, getUser.Data),
                        message = user.Message,
                        dataUpdate = "#DataTableUser"
                    });
                }
                return Json(new { isValid = false, errorMessage = true, message = getUser.Message });
            }
            return Json(new { isValid = false, errorMessage = true, message = user.Message });
        }
        #endregion

        //---------- Change State Action ----------
        #region ChangeStateAction
        [HttpGet]
        public IActionResult ChangeState(string httpVerb, int userId, int page, int pageSize)
        {
            if (httpVerb == "GET")
            {
                if (CustomExtentionMethods.IsAjaxRequest(HttpContext.Request, httpVerb).IsSuccess)
                {
                    var userChangeState = _userFacad.ChangeStateUserService.ChangeStateUser(userId);
                    if (userChangeState.IsSuccess)
                    {
                        var getUser = _userFacad.GetUserFullListService.Get(new RequestDto<PaginationDto>
                        {
                            Parameter = new PaginationDto
                            {
                                Page = page,
                                PageSize = pageSize,
                                SearchKey = string.Empty
                            }
                        });
                        if (getUser.ActionType == ActionType.Completed || getUser.ActionType == ActionType.Null)
                        {
                            string userListPath = "~/Areas/Portal/Views/User/Components/DataTableUser/DataTableUserViewComponent.cshtml";
                            return Json(new
                            {
                                isValid = true,
                                html = Helper.RenderRazorViewToString(this, userListPath, getUser.Data),
                                message = userChangeState.Message,
                                dataUpdate = "#DataTableUser"
                            });
                        }
                        return Json(new { isValid = false, message = getUser.Message, errorMessage = true });
                    }
                    return Json(new { isValid = false, message = userChangeState.Message, errorMessage = true });
                }
                return Json(new { message = "Ajax process error" });
            }
            return Json(new { message = "Ajax method error" });
        }
        #endregion

        //---------- User In Roll Action ----------

        //---------- User In Group Action ----------

        //---------- User In OrgOpunitPos Action ----------

    }
}
