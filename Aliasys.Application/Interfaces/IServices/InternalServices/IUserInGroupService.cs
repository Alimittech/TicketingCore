using Aliasys.Domain.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliasys.Application.IServices.InternalServices
{
    public interface IUserInGroupService
    {
        List<UserInGroup> GetUserInGroupWithGroupId (int groupId);
    }
}
