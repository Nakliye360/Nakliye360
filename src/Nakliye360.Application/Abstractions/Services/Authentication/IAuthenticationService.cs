using Nakliye360.Application.Models.DTOs.Authentication.RequestModel;

namespace Nakliye360.Application.Abstractions.Services.Authentication;

public interface IAuthenticationService : IInternalAuthenticationService, IExternalAuthenticationService
{
    Task PasswordResetAsnyc(PasswordResetRequestModel requestModel);
    Task<bool> VerifyResetTokenAsync(string resetToken, string userId);

}
