using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceCategoryServices.Commands.CreateServiceCategory;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceCategoryServices.Commands.UpdateServiceCategory;
using Aliasys.Application.Services.InternalServices.UserServices.FacadPatterns;
using Aliasys.Common.Dtos;
using Aliasys.Common.ExtentionMethods;
using Aliasys.Common.Paginations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.Controllers
{
    [Area(nameof(Portal))]
    [Authorize]
    public class ServiceCategoryController : Controller
    {
        private readonly IServiceCategoryFacad _serviceCategoryFacad;
        public ServiceCategoryController(IServiceCategoryFacad serviceCategoryFacad)
        {
            _serviceCategoryFacad = serviceCategoryFacad;
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

        //---------- List Action ----------
        #region List Action
        [HttpGet]
        public IActionResult List(string searchKey, int page, int pageSize)
        {
            var getServiceCat = _serviceCategoryFacad.GetServiceCategoryFullListService.Get(new RequestDto<PaginationDto>
            {
                Parameter = new PaginationDto
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchKey = searchKey
                }
            });
            if (getServiceCat.ActionType == ActionType.Completed || getServiceCat.ActionType == ActionType.Null)
            {
                string serviceCatListPath = "~/Areas/Portal/Views/ServiceCategory/Components/DataTableServiceCategory/DataTableServiceCategoryViewComponent.cshtml";
                return Json(new
                {
                    isValid = true,
                    html = Helper.RenderRazorViewToString(this, serviceCatListPath, getServiceCat.Data),
                    message = getServiceCat.Message,
                    dataUpdate = "#DataTableServiceCategory"
                });
            }
            return Json(new { isValid = false, message = getServiceCat.Message, errorMessage = true });
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
                    return ViewComponent("CreateServiceCategory");
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Create(RequestCreateServiceCategoryDto request)
        {
            if (ModelState.IsValid)
            {
                var createServiceCategory = _serviceCategoryFacad.CreateServiceCategoryService.Create(new RequestDto<RequestCreateServiceCategoryDto>
                {
                    Parameter = new RequestCreateServiceCategoryDto
                    {
                        Name = request.Name,
                        UserGroupId_FK = request.UserGroupId_FK,
                    }
                });
                if (createServiceCategory.IsSuccess)
                {
                    var getServiceCategory = _serviceCategoryFacad.GetServiceCategoryFullListService.Get(new RequestDto<PaginationDto>
                    {
                        Parameter = new PaginationDto
                        {
                            Page = 1,
                            PageSize = 5,
                            SearchKey = string.Empty
                        }
                    });
                    if (getServiceCategory.ActionType == ActionType.Completed || getServiceCategory.ActionType == ActionType.Null)
                    {
                        string serviceCatListPath = "~/Areas/Portal/Views/ServiceCategory/Components/DataTableServiceCategory/DataTableServiceCategoryViewComponent.cshtml";
                        return Json(new
                        {
                            isValid = true,
                            html = Helper.RenderRazorViewToString(this, serviceCatListPath, getServiceCategory.Data),
                            message = createServiceCategory.Message,
                            dataUpdate = "#DataTableServiceCategory"
                        });
                    }
                }
                return Json(new { isValid = false, message = createServiceCategory.Message, errorMessage = true });
            }
            string serviceCatCreatePath = "~/Areas/Portal/Views/ServiceCategory/Components/CreateServiceCategory/CreateServiceCategoryViewComponent.cshtml";
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, serviceCatCreatePath, request) });
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
                    var serviceCatFind = _serviceCategoryFacad.FindServiceCategoryWithIdService.Find(new RequestDto<int>
                    {
                        Parameter = Id
                    });

                    if (serviceCatFind.IsSuccess)
                    {
                        var model = new RequestUpdateServiceCategoryDto
                        {
                            Id = serviceCatFind.Data.Id,
                            Name = serviceCatFind.Data.Name,
                            UserGroupId_FK = serviceCatFind.Data.UserGroupId_FK
                        };
                        return ViewComponent("UpdateServiceCategory", model);
                    }
                    return Json(new { isValid = false, errorMessage = true, message = serviceCatFind.Message });
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Update(RequestUpdateServiceCategoryDto request)
        {
            if (ModelState.IsValid)
            {
                var updateServiceCat = _serviceCategoryFacad.UpdateServiceCategoryService.Update(new RequestDto<RequestUpdateServiceCategoryDto>
                {
                    Parameter = request
                });
                if (updateServiceCat.IsSuccess)
                {
                    var getServiceCat = _serviceCategoryFacad.GetServiceCategoryFullListService.Get(new RequestDto<PaginationDto>
                    {
                        Parameter = new PaginationDto
                        {
                            Page = 1,
                            PageSize = 5,
                            SearchKey = string.Empty
                        }
                    });
                    if (getServiceCat.ActionType == ActionType.Completed || getServiceCat.ActionType == ActionType.Null)
                    {
                        string serviceCatListPath = "~/Areas/Portal/Views/ServiceCategory/Components/DataTableServiceCategory/DataTableServiceCategoryViewComponent.cshtml";
                        return Json(new
                        {
                            isValid = true,
                            html = Helper.RenderRazorViewToString(this, serviceCatListPath, getServiceCat.Data),
                            message = updateServiceCat.Message,
                            dataUpdate = "#DataTableServiceCategory"
                        });
                    }
                    return Json(new { isValid = false, message = getServiceCat.Message, errorMessage = true });
                }
                return Json(new { isValid = false, message = updateServiceCat.Message, errorMessage = true });
            }
            string serviceCatUpdatePath = "~/Areas/Portal/Views/ServiceCategory/Components/UpdateServiceCategory/UpdateServiceCategoryViewComponent.cshtml";
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, serviceCatUpdatePath, request) });
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
                    ViewBag.RouteAction = "deleteServiceCategory";
                    return PartialView("_PartialDelete", Id);
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var serviceCat = _serviceCategoryFacad.DeleteServiceCategoryService.Delete(Id);
            if (serviceCat.IsSuccess)
            {
                var getServiceCat = _serviceCategoryFacad.GetServiceCategoryFullListService.Get(new RequestDto<PaginationDto>
                {
                    Parameter = new PaginationDto
                    {
                        Page = 1,
                        PageSize = 5,
                        SearchKey = string.Empty
                    }
                });
                if (getServiceCat.ActionType == ActionType.Completed || getServiceCat.ActionType == ActionType.Null)
                {
                    string serviceCatListPath = "~/Areas/Portal/Views/ServiceCategory/Components/DataTableServiceCategory/DataTableServiceCategoryViewComponent.cshtml";
                    return Json(new
                    {
                        isValid = true,
                        html = Helper.RenderRazorViewToString(this, serviceCatListPath, getServiceCat.Data),
                        message = serviceCat.Message,
                        dataUpdate = "#DataTableServiceCategory"
                    });
                }
                return Json(new { isValid = false, errorMessage = true, message = getServiceCat.Message });
            }
            return Json(new { isValid = false, errorMessage = true, message = serviceCat.Message });
        }
        #endregion

        //---------- Check Action ----------
        #region Check Action
        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckServiceCatName(string Name)
        {
            var result = _serviceCategoryFacad.FindServiceCategoryWithNameService.Find(new RequestDto<string>
            {
                Parameter = Name
            });
            if (result.IsSuccess)//ServiceCategoryName is Exist!
            {
                return Json(result.Message);
            }
            return Json(true);
        }
        #endregion
    }
}
