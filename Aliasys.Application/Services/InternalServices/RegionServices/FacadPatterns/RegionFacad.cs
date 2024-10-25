using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.RegionServices.Queries.GetRegionSelectList;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aliasys.Application.Services.InternalServices.RegionServices.FacadPatterns
{
    public class RegionFacad : IRegionFacad
    {
        private readonly IDataBaseContext _context;
        public RegionFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private IGetService<List<SelectListItem>> _getRegionSelectListService;
        public IGetService<List<SelectListItem>> GetRegionSelectListService
        {
            get
            {
                return _getRegionSelectListService = _getRegionSelectListService ?? new GetRegionSelectListService(_context);
            }
        }
    }
}
