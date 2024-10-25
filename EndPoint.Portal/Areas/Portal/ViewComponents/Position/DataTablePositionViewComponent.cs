using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.Position
{
    public class DataTablePositionViewComponent : ViewComponent
    {
        private readonly IPositionFacad _positionFacad;
        public DataTablePositionViewComponent(IPositionFacad positionFacad)
        {
            _positionFacad = positionFacad;
        }

        public IViewComponentResult Invoke(string searchKey, int page, int pageSize)
        {
            var posList = _positionFacad.GetPositionFullListService.Get(new RequestDto<PaginationDto>
            {
                Parameter = new PaginationDto
                {
                    SearchKey = searchKey,
                    Page = page,
                    PageSize = pageSize
                }
            });
            return View("~/Areas/Portal/Views/Position/Components/DataTablePosition/DataTablePositionViewComponent.cshtml", posList.Data);
        }
    }
}
