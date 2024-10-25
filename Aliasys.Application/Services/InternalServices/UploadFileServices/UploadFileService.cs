using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.UploadFileServices
{
    public class UploadFileService : IUploadFileService
    {
        private readonly IHostingEnvironment _appEnvironment;
        public UploadFileService(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }
        
        public ResultDto<string> UploadFiles(IFormFile file)
        {
            try
            {
                var size = file.Length;
                string filepath = string.Empty;
                if (size > 0 && size < 1000000)
                {
                    
                    string filename = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                    filepath = Path.Combine(_appEnvironment.WebRootPath + "\\upload\\ticket-upload", filename);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return new ResultDto<string>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Message = "File uploaded succesfully!",
                        Data = filename
                    };
                }
                return new ResultDto<string>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Failed,
                    Message = "The size of the selected file is more than 1 MB"
                };
            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<string>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<string>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "The application is not accessible!",
                };
            }
            catch (Exception ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<string>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<string>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
