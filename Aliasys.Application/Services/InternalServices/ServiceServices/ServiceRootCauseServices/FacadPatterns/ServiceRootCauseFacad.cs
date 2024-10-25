using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRootCauseServices.Queries.GetRootCause.GetRootCauseName;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRootCauseServices.Queries.GetRootCause.GetRootCauseSelectList;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRootCauseServices.FacadPatterns
{
    public class ServiceRootCauseFacad : IServiceRootCauseFacad
    {
        private readonly IDataBaseContext _context;
        public ServiceRootCauseFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private IGetService<List<SelectListItem>> _getServiceRootCauseService;
        public IGetService<List<SelectListItem>> GetServiceRootCauseService
        {
            get
            {
                return _getServiceRootCauseService = _getServiceRootCauseService ?? new GetServiceRootCauseService(_context);
            }
        }

        private IGetService<string, int> _getRootCauseWithIdService;
        public IGetService<string, int> GetRootCauseWithIdService
        {
            get
            {
                return _getRootCauseWithIdService = _getRootCauseWithIdService ?? new GetRootCauseWithIdService(_context);
            }
        }
    }
}
