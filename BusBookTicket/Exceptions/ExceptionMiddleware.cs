using BusBookTicket.Auth.Exceptions;
using BusBookTicket.Auth.Utils;
using BusBookTicket.Common.Common;
using BusBookTicket.Common.Common.Exceptions;
using BusBookTicket.CustomerManage.Exceptions;
using Microsoft.AspNetCore.Authentication.OAuth;
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
            catch (CustomerException ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new Response<string>(true, ex.message)));
            }
            catch(UnauthorizedAccessException ex)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new Response<string>(true, ex.Message)));

            }
            catch(NotFoundException ex)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new Response<string>(true, ex.message)));
            }
            catch(UnAuthorException ex)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new Response<string>(true, ex.Message)));
            }
            catch(NullReferenceException ex)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new Response<string>(true, "NotFound")));
            }
            catch(Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new Response<string>(true, ex.Message)));
            }
        }
    }
}
