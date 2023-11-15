using BusBookTicket.Core.Common;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SendGrid.Helpers.Errors.Model;

namespace BusBookTicket.Exceptions;

public class ValidationFilterAttribute : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            throw new Exception("sdfsdg");
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}