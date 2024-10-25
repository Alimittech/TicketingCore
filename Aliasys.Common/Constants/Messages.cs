using Aliasys.Common.Dtos;

namespace Aliasys.Common.Constants
{
    public static class Messages
    {
        public static ResultDto ShowMessages(MessageTitleType messageTitle)
        {
            int code = 0;
            string message = string.Empty;
            switch (messageTitle)
            {
                //----- Request -----
                case MessageTitleType.Request_Create:
                    code = 201;
                    message = "Added Successfully!";
                    break;
                case MessageTitleType.Request_Update:
                    code = 204;
                    message = "Updated Successfully!";
                    break;
                case MessageTitleType.Request_Delete:
                    code = 204;
                    message = "Deleted Successfully!";
                    break;
                case MessageTitleType.Request_Change:
                    code = 201;
                    message = "Changed Successfully!";
                    break;
                case MessageTitleType.Request_Find:
                    code = 303;
                    message = "Item(s) Found!";
                    break;
                case MessageTitleType.Request_Get:
                    code = 303;
                    message = "Get All Done!";
                    break;
                case MessageTitleType.Request_Fail:
                    code = 304;
                    message = "Action Failed!";
                    break;
                case MessageTitleType.Request_NotChanged:
                    code = 304;
                    message = "Not Changed";
                    break;
                //----- Url -----
                case MessageTitleType.Url_BadRequest:
                    code = 400;
                    message = "Bad Request!";
                    break;
                case MessageTitleType.Url_Unauthorized:
                    code = 401;
                    message = "User Unauthorized!";
                    break;
                case MessageTitleType.Url_Forbidden:
                    code = 403;
                    message = "Forbidden!";
                    break;
                case MessageTitleType.Url_NotFound:
                    code = 404;
                    message = "Page Not Found!";
                    break;
                //----- Server -----
                case MessageTitleType.Server_InternalServerError:
                    code = 500;
                    message = "Internal Server Error!";
                    break;
                case MessageTitleType.Server_ServiceUnavailable:
                    code = 503;
                    message = "Service Unavailable!";
                    break;
                case MessageTitleType.Server_NetworkAuthenticationRequired:
                    code = 511;
                    message = "Network Authentication Required!";
                    break;
                //----- Session -----
                case MessageTitleType.Session_SessionExpired:
                    code = 512;//must be change
                    message = "Session Expired!";
                    break;
                //----- Validation -----
                case MessageTitleType.Valid_ValidationError:
                    code = 512;
                    message = "Validation Error!";
                    break;
                //----- SMS -----
                case MessageTitleType.Sms_SendSmsFailed:
                    code = 512;//must be change
                    message = "Sms Send Successfully!";
                    break;
            }
            return new ResultDto
            {
                Code = code,
                Message = message
            };
        }
    }

    public enum MessageTitleType
    {
        Request_Create = 0,//201
        Request_Update = 1,//204
        Request_Delete = 2,//204
        Request_Change,//201
        Request_Find,
        Request_Get,
        Request_NotFound,
        Request_Fail,
        Request_Error,
        Request_NotChanged,
        //------------
        Url_BadRequest,//400
        Url_Unauthorized,//401
        Url_Forbidden,//403
        Url_NotFound,//404
        //------------
        Server_InternalServerError,//500
        Server_ServiceUnavailable,//503
        Server_NetworkAuthenticationRequired,//511
        //------------
        Session_SessionExpired,
        //------------
        Valid_ValidationError,//512
        //------------
        Sms_SendSmsFailed
    }
}
