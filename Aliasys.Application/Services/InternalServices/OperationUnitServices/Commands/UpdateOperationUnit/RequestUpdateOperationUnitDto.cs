using System.ComponentModel.DataAnnotations;

namespace Aliasys.Application.Services.InternalServices.OperationUnitServices.Commands.UpdateOperationUnit
{
    public class RequestUpdateOperationUnitDto
    {
        public int Id { get; set; }

        [Required]
        //[Remote("checkOperationUnitName")]
        public string Name { get; set; }

        [Required]
        //[Remote("checkOperationUnitCode")]
        public int Code { get; set; }
    }
}
