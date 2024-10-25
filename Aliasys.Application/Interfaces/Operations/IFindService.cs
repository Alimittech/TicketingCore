using Aliasys.Common.Dtos;

namespace Aliasys.Application.Interfaces.Operations
{
    public interface IFindService<T, K>
    {
        ResultDto<T> Find(RequestDto<K> request);
    }

    public interface IFindService<T>
    {
        ResultDto<T> Find(int Id);
    }

    public interface IFindService
    {
        ResultDto Find(string Name);
    }
}
