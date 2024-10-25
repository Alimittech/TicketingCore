using Aliasys.Common.Dtos;

namespace Aliasys.Application.Interfaces.Operations
{
    public interface IChangeService<T>
    {
        ResultDto Change(RequestDto<T> request);
    }
}
