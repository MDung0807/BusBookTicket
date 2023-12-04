using FluentValidation.Results;

namespace BusBookTicket.Core.Common.Exceptions;

public class ValidatorException : ExceptionDetail
{
    public List<string> Errors { get; private set; }
    public ValidatorException(List<ValidationFailure> errors)
    {
        Errors = new List<string>();
        foreach (var item in errors)
        {
            Errors.Add(item.PropertyName + " "+ item.ErrorMessage);
        }
    }
    
    public ValidatorException(string message): base(message)
    {
    }
  
}