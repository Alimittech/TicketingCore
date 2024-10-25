namespace EndPoint.Portal.Areas.Portal.Models.ViewModels.RequestViewModel
{
    public class RequestChangeRequest
    {
        public long ServiceRequestId { get; set; }
        public int StateOperationType { get; set; }
        public string Description { get; set; }
        public string? AttachmentFileName { get; set; }
        public int ProcessUserId { get; set; }
        public int AssignedUserId { get; set; }
    }
}
