using Aliasys.Common.Dtos;

namespace Aliasys.Application.Interfaces.Operations
{
    public interface IUpdateService<T, K>
    {
        ResultDto<T> Update(RequestDto<K> request);
    }
}
