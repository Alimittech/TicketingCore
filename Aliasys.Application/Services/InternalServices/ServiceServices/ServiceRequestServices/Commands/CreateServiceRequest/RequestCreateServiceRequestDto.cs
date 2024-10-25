using Aliasys.Domain.Entities.ServiceEntities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Commands.CreateServiceRequest
{
    public class RequestCreateServiceRequestDto
    {
        public string RequestNumber { get; set; }

        public int OwnerUserId { get; set; }

        [Required]
        [Display(Name="Category")]
        public int ServiceCategoryId { get; set; }

        [Required]
        [Display(Name="Request Type")]
        public int ServiceRequestTypeId { get; set; }

        [Required]
        [Display(Name="Priority")]
        public ServicePriority ServicePriority { get; set; }

        [Required]
        [Display(Name="Occour DateTime")]
        public DateTime OccurDateTime { get; set; }

        [Required]
        [Display(Name="Service Affected")]
        public string ServiceAffected { get; set; }

        [Required]
        [Display(Name="Impact On")]
        public ImpactOn ImpactOn { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string? AttachmentFileName { get; set; }

        [Display(Name ="File Attachment")]
        public IFormFile? FileAttachment { get; set; }

        public  string? ServiceState { get; set; }
    }
}
