using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.GetServiceRequest.GetAllServiceReqInGroupWithUserId;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.ServiceRequestLifeCycle
{
    public class DataTableServiceRequestLifeCycleViewComponent : ViewComponent
    {
        private readonly IServiceRequestFacad _serviceRequestFacad;
        private readonly IUserInGroupFacad _userInGroupFacad;
        public DataTableServiceRequestLifeCycleViewComponent(IServiceRequestFacad serviceRequestFacad, IUserInGroupFacad userInGroupFacad)
        {
            _serviceRequestFacad = serviceRequestFacad;
            _userInGroupFacad = userInGroupFacad;
        }

        public IViewComponentResult Invoke()
        {
            TempDataUserDto userInfo = JsonConvert.DeserializeObject<TempDataUserDto>(TempData["UserInfo"].ToString());
            TempData.Keep();

            var serviceReqList = _serviceRequestFacad.GetAllServiceReqInGroupWithUserIdService.Get(new RequestDto<RequestGetAllServiceReqInGroupDto>
            {
                Parameter = new RequestGetAllServiceReqInGroupDto
                {
                    Pagination = new PaginationDto
                    {
                        Page = 1,
                        PageSize = 5,
                        SearchKey = string.Empty
                    },
                    UserId = userInfo.UserId
                }
            });

            ResultGetAllServiceReqInGroupDto result;
            result = new ResultGetAllServiceReqInGroupDto
            {
                srvRequestList = serviceReqList.Data.srvRequestList ?? null,
                Page = serviceReqList.Data.Page,
                PageSize = serviceReqList.Data.PageSize,
                RowsCount = serviceReqList.Data.RowsCount,
            };

            return View(viewName: "DataTableServiceRequestLifeCycleViewComponent", result);
        }
    }
}
