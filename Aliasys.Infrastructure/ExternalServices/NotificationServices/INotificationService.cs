using Aliasys.Common.Dtos;

namespace Aliasys.Infrastructure.ExternalServices.NotificationServices
{
    public interface INotificationService<T, K>
    {
        //comment
        ResultDto<T> Send(RequestDto<K> request);
    }
}
