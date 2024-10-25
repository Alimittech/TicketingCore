using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;

namespace Aliasys.Infrastructure.ExternalServices.NotificationServices.SmsService
{
    public class SmsService : INotificationService<int?, RequestSmsServiceDto>
    {
        public ResultDto<int?> Send(RequestDto<RequestSmsServiceDto> request)
        {
            try
            {
                bool sendSms = true;
                if (sendSms)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Code = 200,
                        Message = "Sms Send Successfully!"
                    };
                }
                return new ResultDto<int?>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Failed,
                    Code = Messages.ShowMessages(MessageTitleType.Sms_SendSmsFailed).Code,
                    Message = Messages.ShowMessages(MessageTitleType.Sms_SendSmsFailed).Message
                };
            }
            catch (Exception ex)
            {
                return new ResultDto<int?>
                {
                    IsSuccess = false,
                    Message = "System Error" + ex.Message,
                    ActionType = ActionType.Error
                };
            }
        }
    }
}
