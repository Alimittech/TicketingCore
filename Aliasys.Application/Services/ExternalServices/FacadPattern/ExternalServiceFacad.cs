using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Infrastructure.ExternalServices.NotificationServices.EmailService;
using Aliasys.Infrastructure.ExternalServices.NotificationServices.SmsService;
using Aliasys.Infrastructure.ExternalServices.NotificationServices;

namespace Aliasys.Application.Services.ExternalServices.FacadPattern
{
    public class ExternalServiceFacad : IExternalServiceFacad
    {
        //************* IEmailService *************
        private INotificationService<Task, RequestEmailServiceDto> _emailService;
        public INotificationService<Task, RequestEmailServiceDto> EmailService
        {
            get { return _emailService = _emailService ?? new EmailService(); }
        }

        //************* ISmsService *************
        private INotificationService<int?, RequestSmsServiceDto> _smsService;
        public INotificationService<int?, RequestSmsServiceDto> SmsService
        {
            get { return _smsService = _smsService ?? new SmsService(); }
        }
    }
}
