namespace Nakliye360.Application.Exceptions.Authentication
{
    public class PasswordChangeFailedException : Exception
    {
        public PasswordChangeFailedException() : base("An error occurred while updating the password.") // "Şifre güncellenirken bir sorun oluştu."
        {
        }

        public PasswordChangeFailedException(string? message) : base(message)
        {
        }

        public PasswordChangeFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
