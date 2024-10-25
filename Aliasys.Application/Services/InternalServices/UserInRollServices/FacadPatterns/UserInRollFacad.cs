using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.UserInRollServices.Queries.GetUserInRoll.GetUserInRollWithUserName;

namespace Aliasys.Application.Services.InternalServices.UserInRollServices.FacadPatterns
{
    public class UserInRollFacad : IUserInRollFacad
    {
        private readonly IDataBaseContext _context;
        public UserInRollFacad(IDataBaseContext context)
        {
            _context = context; 
        }

        private IGetService<List<ResultGetUserInRollWithUserNameDto>, string> _getUserInRollWithUserNameService;
        public IGetService<List<ResultGetUserInRollWithUserNameDto>, string> GetUserInRollWithUserNameService
        {
            get
            {
                return _getUserInRollWithUserNameService = _getUserInRollWithUserNameService ?? new GetUserInRollWithUserNameService(_context);
            }
        }
    }
}
