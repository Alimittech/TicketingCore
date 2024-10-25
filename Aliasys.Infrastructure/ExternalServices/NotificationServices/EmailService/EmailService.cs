using Aliasys.Common.Dtos;
using System.Net.Mail;
using System.Net;
using System.Text;
using Aliasys.Domain.Entities.UserEntities;

namespace Aliasys.Infrastructure.ExternalServices.NotificationServices.EmailService
{
    //public class EmailService : INotificationService<Task, RequestEmailServiceDto>
    //{
    //    public ResultDto<Task> Send(RequestDto<RequestEmailServiceDto> request)
    //    {
    //        try
    //        {
    //            SmtpClient client = new SmtpClient();
    //            client.Port = 587;
    //            client.Host = "smtp.gmail.com";
    //            client.EnableSsl = true;
    //            client.Timeout = 1000000;
    //            client.DeliveryMethod = SmtpDeliveryMethod.Network;
    //            client.UseDefaultCredentials = false;
    //            client.Credentials = new NetworkCredential("ali.moosavi.software@gmail.com", "alim1020315");
    //            MailMessage mailMessage = new MailMessage("ali.moosavi.software@gmail.com",
    //                request.Parameter.UserEmail, request.Parameter.Subject, request.Parameter.Body);
    //            mailMessage.IsBodyHtml = true;
    //            mailMessage.BodyEncoding = Encoding.UTF8;
    //            mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
    //            client.Send(mailMessage);
    //            return new ResultDto<Task>
    //            {
    //                IsSuccess = true,
    //                ActionType = ActionType.Completed,
    //                Data = Task.CompletedTask
    //            };
    //        }
    //        catch (Exception ex)
    //        {
    //            return new ResultDto<Task>
    //            {
    //                IsSuccess = false,
    //                ActionType = ActionType.Error,
    //                Message = "خطایی رخ داده است" + ex.Message
    //            };
    //        }

    //    }
    //}


    public class EmailService : INotificationService<Task, RequestEmailServiceDto>
    {
        public ResultDto<Task> Send(RequestDto<RequestEmailServiceDto> request)
        {
            try
            {
                SmtpClient client = new SmtpClient
                {
                    Port = 587, 
                    Host = "mail.aliasaas.com",
                    EnableSsl = false,
                    Timeout = 1000000,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,

                    Credentials = new System.Net.NetworkCredential("BcSupport@aliasaas.com", "AWh}ZJCxiUQD")
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("BcSupport@aliasaas.com"),
                    To = { request.Parameter.UserEmail },
                    Subject = request.Parameter.Subject,
                    Body = request.Parameter.Body,
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8,
                    DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess
                };

                client.Send(mailMessage);
                return new ResultDto<Task>
                {
                    IsSuccess = true,
                    ActionType = ActionType.Completed,
                    Data = Task.CompletedTask
                };
            }
            catch (Exception ex)
            {
                LogError(ex);

                return new ResultDto<Task>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "خطایی رخ داده است: " + ex.Message
                };
            }
        }

        private void LogError(Exception ex)
        {
            // اینجا کد لاگ کردن خطا ادد بشه
        }

    }
    }
