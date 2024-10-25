using Aliasys.Domain.Entities.BaseEntities;

namespace Aliasys.Domain.Entities.UserEntities
{
    public class UserRoll : BaseEntity<int>
    {
        public override int Id { get; set; }
        public string RollName { get; set; }
        public string Description { get; set; }
    }
    #region Comment
    //CRUD ===> Full Access
    //CRU  ===> Create Read Update
    //CRD  ===> Create Read Delete
    //CR   ===> Create Read
    //RUD  ===> Read Update Delete
    //RU   ===> Read Update
    //RD   ===> Read Delete
    //R    ===> Read Only
    //None ===> None
    #endregion
}
