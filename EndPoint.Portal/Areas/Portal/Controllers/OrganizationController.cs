using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Services.InternalServices.OrganizationServices.Commands.CreateOrganization;
using Aliasys.Application.Services.InternalServices.OrganizationServices.Commands.UpdateOrganization;
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
    public class OrganizationController : BaseController
    {
        private readonly IOrganizationFacad _organizationFacad;
        public OrganizationController(IOrganizationFacad organizationFacad)
        {
            _organizationFacad = organizationFacad;
        }

        //---------- Index Action ----------
        #region Index Action
        [HttpGet]
        public IActionResult Index(PaginationDto paginationDto)
        {
            ViewBag.searchKey = paginationDto.SearchKey;
            ViewBag.page = paginationDto.Page;
            ViewBag.pageSize = paginationDto.PageSize;
            return View();
        }
        #endregion

        #region List Action
        //---------- List Action ----------
        [HttpGet]
        public IActionResult List(string searchKey, int page, int pageSize)
        {
            var getOrg = _organizationFacad.GetOrganizationFullListService.Get(new RequestDto<PaginationDto>
            {
                Parameter = new PaginationDto
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchKey = searchKey
                }
            });
            if (getOrg.ActionType == ActionType.Completed || getOrg.ActionType == ActionType.Null)
            {
                string orgListPath = "~/Areas/Portal/Views/Organization/Components/DataTableOrganization/DataTableOrganizationViewComponent.cshtml";
                return Json(new
                {
                    isValid = true,
                    html = Helper.RenderRazorViewToString(this, orgListPath, getOrg.Data),
                    message = getOrg.Message,
                    dataUpdate = "#resultDataTableOrganization"
                });
            }
            return Json(new { isValid = false, message = getOrg.Message, errorMessage = true });
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
                    return ViewComponent("CreateOrganization");
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Create(RequestCreateOrganizationDto request)
        {
            if (ModelState.IsValid)
            {
                var createOrg = _organizationFacad.CreateOrganizationService.Create(new RequestDto<RequestCreateOrganizationDto>
                {
                    Parameter = request
                });
                if (createOrg.IsSuccess)
                {
                    var getOrg = _organizationFacad.GetOrganizationFullListService.Get(new RequestDto<PaginationDto>
                    {
                        Parameter = new PaginationDto
                        {
                            Page = 1,
                            PageSize = 5,
                            SearchKey = string.Empty
                        }
                    });
                    if (getOrg.ActionType == ActionType.Completed || getOrg.ActionType == ActionType.Null)
                    {
                        string orgListPath = "~/Areas/Portal/Views/Organization/Components/DataTableOrganization/DataTableOrganizationViewComponent.cshtml";
                        return Json(new
                        {
                            isValid = true,
                            html = Helper.RenderRazorViewToString(this, orgListPath, getOrg.Data),
                            message = createOrg.Message,
                            dataUpdate = "#DataTableOrganization"
                        });
                    }
                }
                //ModelState.AddModelError(request.Name, "The entered name is already exist!");
                return Json(new { isValid = false, message = createOrg.Message, errorMessage = true });
            }
            string OrgCreatePath = "~/Areas/Portal/Views/Organization/Components/CreateOrganization/CreateOrganizationViewComponent.cshtml";
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, OrgCreatePath, request) });
        }
        #endregion

        //---------- Check Action ----------
        #region Check Action
        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckOrganizationName(string Name)
        {
            var result = _organizationFacad.FindOrganizationWithNameService.Find(new RequestDto<string>
            {
                Parameter = Name
            });
            if (result.IsSuccess)//OrganizationName is Exist!
            {
                return Json(result.Message);
            }
            return Json(true);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckSameOrganizationNameForUpdate(int Id, string Name)
        {
            return View();
        }
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
                    var orgFind = _organizationFacad.FindOrganizationWithIdService.Find(new RequestDto<int>
                    {
                        Parameter = Id
                    });

                    if (orgFind.IsSuccess)
                    {
                        var model = new RequestUpdateOrganizationDto
                        {
                            Id = orgFind.Data.Id,
                            ParentOrganization = int.Parse(orgFind.Data.ParentOrganization.Value),
                            Name = orgFind.Data.Name,
                            DomainName = orgFind.Data.DomainName,
                            Region = int.Parse(orgFind.Data.Region.Value),
                            Address = orgFind.Data.Address,
                            Phone = orgFind.Data.Phone
                        };
                        return ViewComponent("UpdateOrganization", model);
                    }
                    return Json(new { isValid = false, errorMessage = true, message = orgFind.Message });
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Update(RequestUpdateOrganizationDto request)
        {
            if (ModelState.IsValid)
            {
                var updateOrg = _organizationFacad.UpdateOrganizationService.Update(new RequestDto<RequestUpdateOrganizationDto>
                {
                    Parameter = request
                });
                if (updateOrg.IsSuccess)
                {
                    var getOrg = _organizationFacad.GetOrganizationFullListService.Get(new RequestDto<PaginationDto>
                    {
                        Parameter = new PaginationDto
                        {
                            Page = 1,
                            PageSize = 5,
                            SearchKey = string.Empty
                        }
                    });
                    if (getOrg.ActionType == ActionType.Completed || getOrg.ActionType == ActionType.Null)
                    {
                        string orgListPath = "~/Areas/Portal/Views/Organization/Components/DataTableOrganization/DataTableOrganizationViewComponent.cshtml";
                        return Json(new
                        {
                            isValid = true,
                            html = Helper.RenderRazorViewToString(this, orgListPath, getOrg.Data),
                            message = updateOrg.Message,
                            dataUpdate = "#DataTableOrganization"
                        });
                    }
                    return Json(new { isValid = false, message = getOrg.Message, errorMessage = true });
                }
                return Json(new { isValid = false, message = updateOrg.Message, errorMessage = true });
            }
            string orgUpdatePath = "~/Areas/Portal/Views/Organization/Components/UpdateOrganization/UpdateOrganizationViewComponent.cshtml";
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, orgUpdatePath, request) });
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
                    ViewBag.RouteAction = "deleteOrganization";
                    return PartialView("_PartialDelete", Id);
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var org = _organizationFacad.DeleteOrganizationService.Delete(Id);
            if (org.IsSuccess)
            {
                var getOrg = _organizationFacad.GetOrganizationFullListService.Get(new RequestDto<PaginationDto>
                {
                    Parameter = new PaginationDto
                    {
                        Page = 1,
                        PageSize = 5,
                        SearchKey = string.Empty
                    }
                });
                if (getOrg.ActionType == ActionType.Completed || getOrg.ActionType == ActionType.Null)
                {
                    string orgListPath = "~/Areas/Portal/Views/Organization/Components/DataTableOrganization/DataTableOrganizationViewComponent.cshtml";
                    return Json(new
                    {
                        isValid = true,
                        html = Helper.RenderRazorViewToString(this, orgListPath, getOrg.Data),
                        message = org.Message,
                        dataUpdate = "#DataTableOrganization"
                    });
                }
                return Json(new { isValid = false, errorMessage = true, message = getOrg.Message });
            }
            return Json(new { isValid = false, errorMessage = true, message = org.Message });
        }
        #endregion
    }
}
