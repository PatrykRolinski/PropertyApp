namespace PropertyApp.Application.Exceptions
{
    public class ResetPasswordException : Exception
    {
        public ResetPasswordException(string? message) : base(message)
        {
        }
    }
}