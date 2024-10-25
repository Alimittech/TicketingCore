using Aliasys.Common.Dtos;

namespace Aliasys.Application.Services.InternalServices.UserServices.Queries.GetUser.ValidateLdapUser
{
    public interface IValidateLdapUser<K>
    {
        ResultDto ValidateUser(K request);
    }
}
