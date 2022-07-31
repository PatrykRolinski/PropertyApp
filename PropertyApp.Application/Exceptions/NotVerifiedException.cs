namespace PropertyApp.Application.Exceptions;

public class NotVerifiedException : Exception
{
    public NotVerifiedException(string? message) : base(message)
    {
    }
}
