using Aliasys.Common.Dtos;

namespace Aliasys.Application.Interfaces.Operations
{
    public interface ICreateService<T, K>
    {
        ResultDto<T> Create(RequestDto<K> request);
    }
}
