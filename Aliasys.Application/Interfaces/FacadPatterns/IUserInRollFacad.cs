using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.UserInRollServices.Queries.GetUserInRoll.GetUserInRollWithUserName;

namespace Aliasys.Application.Interfaces.FacadPatterns
{
    public interface IUserInRollFacad
    {
        IGetService<List<ResultGetUserInRollWithUserNameDto>, string> GetUserInRollWithUserNameService { get; }
    }
}
