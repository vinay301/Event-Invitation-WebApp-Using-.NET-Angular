using EventInvitationWebApp.Models.DTO;

namespace EventInvitationWebApp.Services.Interface
{
    public interface IAuthService
    {
        Task<String> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
