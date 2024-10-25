﻿using Aliasys.Application.Interfaces.FacadPatterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.ServiceState
{
    public class CreateServiceStateViewComponent : ViewComponent
    {
        private readonly IServiceRequestTypeFacad _serviceRequestTypeFacad;
        public CreateServiceStateViewComponent(IServiceRequestTypeFacad serviceRequestTypeFacad)
        {
            _serviceRequestTypeFacad = serviceRequestTypeFacad;
        }

        public IViewComponentResult Invoke()
        {
            var serviceReqTypeList = _serviceRequestTypeFacad.GetServiceRequestTypeSelectListService.Get().Data;
            if (serviceReqTypeList != null)
            {
                ViewBag.ServiceReqTypeList = new SelectList(serviceReqTypeList, "Value", "Text", 0);
            }
            return View("CreateServiceStateViewComponent");
        }
    }
}
