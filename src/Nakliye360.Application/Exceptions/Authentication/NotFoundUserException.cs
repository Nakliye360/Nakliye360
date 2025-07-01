namespace Nakliye360.Application.Exceptions.Authentication
{
    public class NotFoundUserException : Exception
    {
        public NotFoundUserException() : base("User name or password is incorrect.") //  Kullanıcı adı veya şifre hatalı.
        {
        }

        public NotFoundUserException(string? message) : base(message)
        {
        }

        public NotFoundUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
