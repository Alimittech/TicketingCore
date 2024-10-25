using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.OperationUnit
{
    public class DataTableOperationUnitViewComponent : ViewComponent
    {
        private readonly IOperationUnitFacad _oPerationUnitFacad;
        public DataTableOperationUnitViewComponent(IOperationUnitFacad operationUnitFacad)
        {
            _oPerationUnitFacad = operationUnitFacad;
        }

        public IViewComponentResult Invoke(string searchKey, int page, int pageSize)
        {
            var opunitList = _oPerationUnitFacad.GetOperationUnitFullListService.Get(new RequestDto<PaginationDto>
            {
                Parameter = new PaginationDto
                {
                    SearchKey = searchKey,
                    Page = page,
                    PageSize = pageSize
                }
            });
            return View("~/Areas/Portal/Views/OperationUnit/Components/DataTableOperationUnit/DataTableOperationUnitViewComponent.cshtml", opunitList.Data);
        }
    }
}
