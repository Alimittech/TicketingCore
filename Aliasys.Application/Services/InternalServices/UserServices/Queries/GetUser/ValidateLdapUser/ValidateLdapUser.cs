using Aliasys.Common.Dtos;
using Aliasys.Common.Log;
using Newtonsoft.Json;
using Novell.Directory.Ldap;

namespace Aliasys.Application.Services.InternalServices.UserServices.Queries.GetUser.ValidateLdapUser
{
    public class ValidateLdapUser : IValidateLdapUser<RequestValidateLdapUserDto>
    {
        public ResultDto ValidateUser(RequestValidateLdapUserDto request)
        {
            /* change active Directory */

            return new ResultDto
            {
                IsSuccess = true,
                ActionType = ActionType.Completed,
                Message = "Login Successfully!"
            };

            string userDn = $"{request.UserName}@{request.DomainName}";
            try
            {
                using (var connection = new LdapConnection { SecureSocketLayer = false })
                {
                    connection.Connect(request.DomainName, LdapConnection.DefaultPort);
                    connection.Bind(userDn, request.Password);
                    if (connection.Bound)
                    {
                        return new ResultDto
                        {
                            IsSuccess = true,
                            ActionType = ActionType.Completed,
                            Message = "Login Successfully!"
                        };
                    }
                    else
                    {
                        FileLogger.Info("ValidateLdapUser", "ValidateUser", $"connection not bound:{JsonConvert.SerializeObject(request)}");
                    }
                }
            }
            catch (LdapException ex)
            {
                FileLogger.Error("ValidateLdapUser", "ValidateUser", $"request:{JsonConvert.SerializeObject(request)}{Environment.NewLine}exception:{ex}");
                // Log exception
            }
            return new ResultDto
            {
                IsSuccess = false,
                ActionType = ActionType.Failed,
                Message = "The username or password is incorrect!"
            };
        }
    }
}
