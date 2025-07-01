namespace Nakliye360.Application.Exceptions.Authentication
{
    public class AuthenticationErrorException : Exception
    {
        public AuthenticationErrorException() : base("Identity authentication error") 
        {
        }

        public AuthenticationErrorException(string? message) : base(message)
        {
        }

        public AuthenticationErrorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
