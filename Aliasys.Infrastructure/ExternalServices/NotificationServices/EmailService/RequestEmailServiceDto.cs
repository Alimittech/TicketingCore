namespace Aliasys.Infrastructure.ExternalServices.NotificationServices.EmailService
{
    public class RequestEmailServiceDto
    {
        public string UserEmail { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
    }
}
