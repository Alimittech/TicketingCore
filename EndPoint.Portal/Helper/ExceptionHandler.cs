using Aliasys.Common.Log;

namespace EndPoint.Portal
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate next;

        public ExceptionHandler(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception exp)
            {
                HandleExceptionAsync(context, exp);
            }
        }

        private void HandleExceptionAsync(HttpContext context, Exception exp)
        {
            string action = context.Request.Path.Value.ToLower();
            FileLogger.Error("ExceptionMiddleware", action, exp);
        }
    }
}
