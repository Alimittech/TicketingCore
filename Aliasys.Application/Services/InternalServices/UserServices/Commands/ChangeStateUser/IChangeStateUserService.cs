using Aliasys.Common.Dtos;

namespace Aliasys.Application.Services.InternalServices.UserServices.Commands.ChangeStateUser
{
    public interface IChangeStateUserService 
    {
        ResultDto ChangeStateUser(int UserId);
    }
}
