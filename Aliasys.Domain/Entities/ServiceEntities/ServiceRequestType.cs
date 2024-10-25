using Aliasys.Domain.Entities.BaseEntities;

namespace Aliasys.Domain.Entities.ServiceEntities
{
    public class ServiceRequestType : BaseEntity<int>
    {
        public override int Id { get; set; }
        public RequestType RequestType { get; set; }
        public string Name { get; set; }//Trouble Ticket
        public string BriefName { get; set; }//TT
    }

    public enum RequestType
    {
        SW_Truoble_Ticket = 1,
        SW_Change_Request = 2,
        SW_Technical_Inquery = 3,
        SW_Access_Permision = 4,
        SW_Account_Creation = 5,
        IT_Software_Problem = 6,
        IT_Hardware_Problem = 7,
        IT_Network_Problem = 8
    }
}
