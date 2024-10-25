using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Common.Dtos;
using EndPoint.Portal.Areas.Portal.Models.ViewModels.RequestViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.ServiceRequest
{
    public class ChangeServiceRequestViewComponent : ViewComponent
    {
        private readonly IServiceRequestLifeCycleFacad _serviceReqLifeCycleFacad;
        private readonly IUserInGroupFacad _userInGroupFacad;
        public ChangeServiceRequestViewComponent(IServiceRequestLifeCycleFacad serviceReqLifeCycleFacad)
        {
            _serviceReqLifeCycleFacad = serviceReqLifeCycleFacad;
        }
        public IViewComponentResult Invoke(long Id)//Id is for RequestId
        {
            TempDataUserDto userInfo = JsonConvert.DeserializeObject<TempDataUserDto>(TempData["UserInfo"].ToString());
            TempData.Keep();

            var reqLifeCycle = _serviceReqLifeCycleFacad.GetReqLifeCycleDependencyWithReqIdService.Get(new RequestDto<long>
            {
                Parameter = Id
            });

            var getLastReqRecord = reqLifeCycle.Data.ServiceRequestLifeCycles.LastOrDefault();
            ViewBag.LastReqPhase = getLastReqRecord.PhaseName;
            ViewBag.LastReqState = getLastReqRecord.StateName;
            switch (getLastReqRecord.PhaseName)
            {
                case "Reject":
                    ViewBag.StateOperationType = Enum.GetValues(typeof(StateOperationType)).Cast<StateOperationType>()
                        .Except(new StateOperationType[] { StateOperationType.Confirm })
                        .Select(p => new SelectListItem { Text = p.ToString(), Value = ((int)p).ToString() });
                    break;
                case "Confirm":
                    ViewBag.StateOperationType = Enum.GetValues(typeof(StateOperationType)).Cast<StateOperationType>()
                        .Except(new StateOperationType[] { StateOperationType.Cancel })
                        .Select(p => new SelectListItem { Text = p.ToString(), Value = ((int)p).ToString() });
                    break;
            }

            ChangeRequestViewModel _changeReq = new ChangeRequestViewModel();
            _changeReq.UserInfo = reqLifeCycle.Data.UserInfo;
            _changeReq.ServiceRequest = reqLifeCycle.Data.ServiceRequest;
            _changeReq.ServiceRequestLifeCycles = reqLifeCycle.Data.ServiceRequestLifeCycles;
            _changeReq.ProcessUserId = userInfo.UserId;
            _changeReq.AssignedUserId = userInfo.UserId;
            return View("ChangeServiceRequestViewComponent", _changeReq);
        }
    }

    public enum StateOperationType
    {
        Resubmit = 1,
        Cancel = 2,
        Confirm = 3
    }
    //if Reject  ==> Cancel / Resubmit
    //if Confirm ==> Confirm / Resubmit
}
