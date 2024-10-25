namespace Aliasys.Application.Services.InternalServices.UserServices.Queries.GetUser.ValidateLdapUser
{
    public class RequestValidateLdapUserDto
    {
        public string DomainName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
