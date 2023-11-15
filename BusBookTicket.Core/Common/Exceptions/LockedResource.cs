namespace BusBookTicket.Core.Common.Exceptions;

public class LockedResource : ExceptionDetail
{
    public override string Message { get; }
    public string message { get; private set; }
    public LockedResource(string mess) : base(mess)
    {
        this.message = mess;
    }
    public LockedResource()
    {}
}