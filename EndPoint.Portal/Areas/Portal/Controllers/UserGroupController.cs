using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Services.InternalServices.PositionServices.Commands.UpdatePosition;
using Aliasys.Application.Services.InternalServices.PositionServices.FacadPatterns;
using Aliasys.Application.Services.InternalServices.UserGroupServices.Commands.CreateUserGroup;
using Aliasys.Application.Services.InternalServices.UserGroupServices.Commands.UpdateUserGroup;
using Aliasys.Common.Dtos;
using Aliasys.Common.ExtentionMethods;
using Aliasys.Common.Paginations;
using Azure.Core;
using EndPoint.Portal.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.Controllers
{
    [Area(nameof(Portal))]
    [Authorize]
    public class UserGroupController : BaseController
    {
        private readonly IUserGroupFacad _userGroupFacad;
        public UserGroupController(IUserGroupFacad userGroupFacad)
        {
            _userGroupFacad = userGroupFacad;
        }

        #region Create Action
        [HttpGet]
        public IActionResult Create(string httpVerb)
        {
            if (httpVerb == "GET")
            {
                if (CustomExtentionMethods.IsAjaxRequest(HttpContext.Request, httpVerb).IsSuccess)
                {
                    return ViewComponent("CreateUserGroup");
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Create(RequestCreateUserGroupDto request)
        {
            if (ModelState.IsValid)
            {
                var createUserGroup = _userGroupFacad.CreateUserGroupService.Create(new RequestDto<RequestCreateUserGroupDto>
                {
                    Parameter = new RequestCreateUserGroupDto
                    {
                        GroupName = request.GroupName,
                    }
                });
                if (createUserGroup.IsSuccess)
                {
                    var getUserGroup = _userGroupFacad.GetUserGroupFullListService.Get(new RequestDto<PaginationDto>
                    {
                        Parameter = new PaginationDto
                        {
                            Page = 1,
                            PageSize = 5,
                            SearchKey = string.Empty
                        }
                    });
                    if (getUserGroup.ActionType == ActionType.Completed || getUserGroup.ActionType == ActionType.Null)
                    {
                        string userGroupListPath = "~/Areas/Portal/Views/UserGroup/Components/DataTableUserGroup/DataTableUserGroupViewComponent.cshtml";
                        return Json(new
                        {
                            isValid = true,
                            html = Helper.RenderRazorViewToString(this, userGroupListPath, getUserGroup.Data),
                            message = createUserGroup.Message,
                            dataUpdate = "#DataTableUserGroup"
                        });
                    }
                }
                return Json(new { isValid = false, message = createUserGroup.Message, errorMessage = true });
            }
            string userGroupCreatePath = "~/Areas/Portal/Views/UserGroup/Components/CreateUserGroup/CreateUserGroupViewComponent.cshtml";
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, userGroupCreatePath, request) });
        }
        #endregion

        #region Update Action
        [HttpGet]
        public IActionResult Update(string httpVerb, int Id)
        {
            if (httpVerb == "GET")
            {
                if (CustomExtentionMethods.IsAjaxRequest(HttpContext.Request, httpVerb).IsSuccess)
                {
                    var userGroupFind = _userGroupFacad.FindUserGroupWithIdService.Find(new RequestDto<int>
                    {
                        Parameter = Id
                    });

                    if (userGroupFind.IsSuccess)
                    {
                        var model = new RequestUpdateUserGroupDto
                        {
                            Id = userGroupFind.Data.Id,
                            GroupName = userGroupFind.Data.GroupName,
                        };
                        return ViewComponent("UpdateUserGroup", model);
                    }
                    return Json(new { isValid = false, errorMessage = true, message = userGroupFind.Message });
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Update(RequestUpdateUserGroupDto request)
        {
            if (ModelState.IsValid)
            {
                var updateUserGroup = _userGroupFacad.UpdateUserGroupService.Update(new RequestDto<RequestUpdateUserGroupDto>
                {
                    Parameter = request
                });
                if (updateUserGroup.IsSuccess)
                {
                    var getUserGroup = _userGroupFacad.GetUserGroupFullListService.Get(new RequestDto<PaginationDto>
                    {
                        Parameter = new PaginationDto
                        {
                            Page = 1,
                            PageSize = 5,
                            SearchKey = string.Empty
                        }
                    });
                    if (getUserGroup.ActionType == ActionType.Completed || getUserGroup.ActionType == ActionType.Null)
                    {
                        string userGroupListPath = "~/Areas/Portal/Views/UserGroup/Components/DataTableUserGroup/DataTableUserGroupViewComponent.cshtml";
                        return Json(new
                        {
                            isValid = true,
                            html = Helper.RenderRazorViewToString(this, userGroupListPath, getUserGroup.Data),
                            message = updateUserGroup.Message,
                            dataUpdate = "#DataTableUserGroup"
                        });
                    }
                    return Json(new { isValid = false, message = getUserGroup.Message, errorMessage = true });
                }
                return Json(new { isValid = false, message = updateUserGroup.Message, errorMessage = true });
            }
            string userGroupUpdatePath = "~/Areas/Portal/Views/UserGroup/Components/UpdateUserGroup/UpdateUserGroupViewComponent.cshtml";
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, userGroupUpdatePath, request) });
        }
        #endregion

        #region Delete Action
        [HttpGet]
        public IActionResult Delete(string httpVerb, int Id)
        {
            if (httpVerb == "GET")
            {
                if (CustomExtentionMethods.IsAjaxRequest(HttpContext.Request, httpVerb).IsSuccess)
                {
                    ViewBag.RouteAction = "deleteUserGroup";
                    return PartialView("_PartialDelete", Id);
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var userGroup = _userGroupFacad.DeleteUserGroupService.Delete(Id);
            if (userGroup.IsSuccess)
            {
                var getUserGroup = _userGroupFacad.GetUserGroupFullListService.Get(new RequestDto<PaginationDto>
                {
                    Parameter = new PaginationDto
                    {
                        Page = 1,
                        PageSize = 5,
                        SearchKey = string.Empty
                    }
                });
                if (userGroup.ActionType == ActionType.Completed || userGroup.ActionType == ActionType.Null)
                {
                    string userGroupListPath = "~/Areas/Portal/Views/UserGroup/Components/DataTableUserGroup/DataTableUserGroupViewComponent.cshtml";
                    return Json(new
                    {
                        isValid = true,
                        html = Helper.RenderRazorViewToString(this, userGroupListPath, getUserGroup.Data),
                        message = userGroup.Message,
                        dataUpdate = "#DataTableUserGroup"
                    });
                }
                return Json(new { isValid = false, errorMessage = true, message = getUserGroup.Message });
            }
            return Json(new { isValid = false, errorMessage = true, message = userGroup.Message });
        }
        #endregion
    }
}
