using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.OperationUnitDependencyServices.Commands.ChangeOpunitDep;
using Aliasys.Application.Services.InternalServices.OperationUnitDependencyServices.Queries.FindOpunitDep;

namespace Aliasys.Application.Services.InternalServices.OperationUnitDependencyServices.FacadPatterns
{
    public class OperationUnitDependencyFacad : IOperationUnitDependencyFacad
    {
        private readonly IDataBaseContext _context;
        public OperationUnitDependencyFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private IFindService<ResultFindOpunitDepDto> _findOpunitDepWithManagerIdService;
        public IFindService<ResultFindOpunitDepDto> FindOpunitDepWithManagerIdService
        {
            get
            {
                return _findOpunitDepWithManagerIdService = _findOpunitDepWithManagerIdService ?? new FindOpunitDepWithManagerIdService(_context);
            }
        }

        private IFindService<ResultFindOpunitDepDto, int> _findOpunitDepWithOpunitIdService;
        public IFindService<ResultFindOpunitDepDto, int> FindOpunitDepWithOpunitIdService
        {
            get
            {
                return _findOpunitDepWithOpunitIdService = _findOpunitDepWithOpunitIdService ?? new FindOpunitDepWithOpunitIdService(_context);
            }
        }

        private IChangeService<RequestChangeOpunitDependencyDto> _changeOpunitDependencyService;
        public IChangeService<RequestChangeOpunitDependencyDto> ChangeOpunitDependencyService
        {
            get
            {
                return _changeOpunitDependencyService = _changeOpunitDependencyService ?? new ChangeOpunitDependencyService(_context);
            }
        }
    }
}
