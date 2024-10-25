
using Aliasys.Domain.Entities.UserEntities;
using Aliasys.Persistence.Repository;
using Aliasys.Application.IServices.InternalServices;

namespace Aliasys.Application.Services.InternalServices.UserInGroupServices.Queries.FindUserInGroup
{
    public class FindUserInGroupCustomService : IUserInGroupService
    {
        private readonly IRepository<UserInGroup> _repository;

        public FindUserInGroupCustomService(IRepository<UserInGroup> repository)
        {
            _repository = repository;
        }

        public List<UserInGroup> GetUserInGroupWithGroupId(int groupId)
        {
            var resutl = _repository.FindAllAsync(u => u.UserGroupId_FK == groupId);
            return resutl.Result.ToList();
        }
    }
}
