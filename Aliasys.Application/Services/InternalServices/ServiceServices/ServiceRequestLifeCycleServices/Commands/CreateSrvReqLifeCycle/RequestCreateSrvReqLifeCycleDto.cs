using Aliasys.Domain.Entities.ServiceEntities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestLifeCycleServices.Commands.CreateSrvReqLifeCycle
{
    public class RequestCreateSrvReqLifeCycleDto
    {
        [Required]
        [Display(Name = "Service Request")]
        public long ServiceRequestId { get; set; }

        [Required]
        [Display(Name = "Service State")]
        public int ServiceStateId { get; set; }

        [Required]
        [Display(Name = "Service Phase")]
        public int ServicePhaseId { get; set; }

        public ProcessAction ProcessAction { get; set; }

        [Display(Name = "Root Cause")]
        public int? RootCauseId { get; set; }

        [Display(Name = "Sub Cause")]
        public int? SubCauseId { get; set; }

        public string Description { get; set; }

        public string? AttachmentFileName { get; set; }

        public IFormFile? FileAttachment { get; set; }

        public int ProcessUserId { get; set; }

        public int AssignedUserId { get; set; }

        public int? AssignedGroupId { get; set; }
    }
}
