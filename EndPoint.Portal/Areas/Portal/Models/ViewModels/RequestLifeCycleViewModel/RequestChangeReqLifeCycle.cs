using Aliasys.Domain.Entities.ServiceEntities;
using System.ComponentModel.DataAnnotations;

namespace EndPoint.Portal.Areas.Portal.Models.ViewModels.RequestLifeCycleViewModel
{
    public class RequestChangeReqLifeCycle
    {
        public long ServiceRequestId { get; set; }
       
        [Required]
        [Display(Name = "Process Action")]
        public ProcessAction? ProcessAction { get; set; }

        public int? RootCause { get; set; }

        public int? SubCause { get; set; }

        [Required]
        public string Description { get; set; }

        public string? AttachmentFileName { get; set; }

        [Display(Name ="File Attachment")]
        public IFormFile? FileAttachment { get; set; }

        public int ProcessUserId { get; set; }

        public int AssignedUserId { get; set; }

        public int? AssignedGroupId { get; set; }
    }
}
