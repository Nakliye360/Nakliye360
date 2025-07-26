namespace Nakliye360.Application.Abstractions.Services.Infrastructure;


public interface IMailService
{
    Task<bool> SendEmailAsync(string to, string subject, string body);
}
