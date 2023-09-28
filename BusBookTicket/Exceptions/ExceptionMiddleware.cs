using BusBookTicket.Auth.Exceptions;
using BusBookTicket.Common.Common;
using Newtonsoft.Json;

namespace BusBookTicket.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(AuthException authException)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new Response<string>(true, authException.message)));

            }
        }
    }
}
