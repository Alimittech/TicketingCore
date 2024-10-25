using Aliasys.Infrastructure.ExternalServices.NotificationServices;
using Aliasys.Infrastructure.ExternalServices.NotificationServices.EmailService;
using Aliasys.Infrastructure.ExternalServices.NotificationServices.SmsService;

namespace Aliasys.Application.Interfaces.FacadPatterns
{
    public interface IExternalServiceFacad
    {
        //    Interface                                 Implementation Class
        INotificationService<Task, RequestEmailServiceDto> EmailService { get; }
        INotificationService<int?, RequestSmsServiceDto> SmsService { get; }
    }
}
