using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusBookTicket.Auth.Exceptions;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.CustomerManage.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SendGrid.Helpers.Errors.Model;
using NotFoundException = BusBookTicket.Core.Common.Exceptions.NotFoundException;

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
            // catch (Exception ex)
            // {
            //     var statusCode = ex switch
            //     {
            //         NotFoundException => (int)HttpStatusCode.NotFound,
            //         UnauthorizedException => (int)HttpStatusCode.Unauthorized,
            //         ValidatorException => (int)HttpStatusCode.BadRequest,
            //         _ => (int)HttpStatusCode.InternalServerError,
            //     };
            //
            //     context.Response.ContentType = "application/json";
            //     await context.Response.WriteAsync(
            //         JsonConvert.SerializeObject(new Response<string>(true, ex.Message)));
            // }
            catch (AuthException authException)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(
                    JsonConvert.SerializeObject(new Response<string>(true, authException.message)));
            
            }
            catch (CustomerException ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new Response<string>(true, ex.message)));
            }
            catch (UnauthorizedAccessException ex)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new Response<string>(true, ex.Message)));
            
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new Response<string>(true, ex.message)));
            }
            catch (UnAuthorException ex)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new Response<string>(true, ex.Message)));
            }
            catch (NullReferenceException ex)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new Response<string>(true, ex.Message)));
            }
            catch (ValidatorException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new Response<List<string>>(true, ex.Errors)));
            }
            catch (BadRequestException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new Response<string>(true, ex.Message)));
            
            }
            catch (LockedResource ex)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new Response<string>(true, ex.message)));
            }
            catch (ExceptionDetail ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new Response<string>(true, ex.message)));
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new Response<string>(true, ex.Message)));
            }
        }
    }
}
