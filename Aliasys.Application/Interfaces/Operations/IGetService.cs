using Aliasys.Common.Dtos;

namespace Aliasys.Application.Interfaces.Operations
{
    public interface IGetService<T, K>
    {
        ResultDto<T> Get(RequestDto<K> request);
    }

    public interface IGetService<T>
    {
        ResultDto<T> Get();
    }

    public interface IGetService
    {
        ResultDto Get();
    }
}
