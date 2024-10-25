using Aliasys.Domain.Entities.BaseEntities;

namespace Aliasys.Domain.Entities.ServiceEntities
{
    public class ServiceRequestLifeCycle : BaseEntity<long>
    {
        public override long Id { get; set; }
        public long ServiceRequestId_FK { get; set; }
        public int ServiceStateId_FK { get; set; }
        public int ServicePhaseId_FK { get; set; }
        public ProcessAction? ProcessAction { get; set; }
        public int? RootCauseId { get; set; }//It is used for update or resolved or rejected 
        public int? SubCauseId { get; set; }//It is used for update or resolved or rejected
        public string Description { get; set; }
        public string? AttachmentFileName { get; set; }
        public int ProcessUserId { get; set; }
        public int AssignedUserId { get; set; }
        public int? AssignedGroupId { get; set; }//It is used to refer to another group
    }

    public enum ProcessAction
    {
        None = 0,
        Accept = 1,
        Reject = 2,
        Resolved = 3,
        Update = 4,
        Assign = 5
    }

    public enum ServiceStateProc
    {
        Draft = 0,
        Running = 1,
        Cancelled = 2,
        Completed = 3,
    }

    public enum ServicePhaseProc
    {
        Creation = 0,
        Handle = 1,
        Process = 2,
        Reject = 3,
        Confirm = 4
    }
}
