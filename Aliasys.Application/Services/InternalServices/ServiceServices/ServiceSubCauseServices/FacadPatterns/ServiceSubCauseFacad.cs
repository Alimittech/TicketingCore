using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceSubCauseServices.Queries.GetSubCause.GetActiveSubCauseSelectList;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceSubCauseServices.Queries.GetSubCause.GetSubCauseName;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceSubCauseServices.Queries.GetSubCause.GetSubCauseSelectList;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceSubCauseServices.FacadPatterns
{
    public class ServiceSubCauseFacad : IServiceSubCauseFacad
    {
        private readonly IDataBaseContext _context;
        public ServiceSubCauseFacad(IDataBaseContext context)
        {
            _context = context;
        }
        private IGetService<List<SelectListItem>> _getServiceSubCauseService;
        public IGetService<List<SelectListItem>> GetServiceSubCauseService
        {
            get
            {
                return _getServiceSubCauseService = _getServiceSubCauseService ?? new GetServiceSubCauseService(_context);
            }
        }

        private IGetService<List<CustomSelectListItem>> _getServiceActiveSubCauseService;
        public IGetService<List<CustomSelectListItem>> GetServiceActiveSubCauseService
        {
            get
            {
                return _getServiceActiveSubCauseService = _getServiceActiveSubCauseService ?? new GetServiceActiveSubCauseService(_context);
            }
        }

        private IGetService<string, int> _getSubCauseWithIdService;
        public IGetService<string, int> GetSubCauseWithIdService
        {
            get
            {
                return _getSubCauseWithIdService = _getSubCauseWithIdService ?? new GetSubCauseWithIdService(_context);
            }
        }
    }
}
