using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Http;

namespace Aliasys.Application.Interfaces.Operations
{
    public interface IUploadFileService
    {
        ResultDto<string> UploadFiles(IFormFile file);
    }
}
