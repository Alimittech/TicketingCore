using Aliasys.Common.Dtos;

namespace Aliasys.Application.Interfaces.Operations
{
    public interface IDeleteService<T, K>
    {
        ResultDto<T> Delete(RequestDto<K> request);
    }

    public interface IDeleteService<K>
    {
        ResultDto Delete(RequestDto<K> request);
    }

    public interface IDeleteService
    {
        ResultDto Delete(int Id);
    }
}
