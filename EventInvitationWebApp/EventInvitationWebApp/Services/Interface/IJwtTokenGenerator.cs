using EventInvitationWebApp.Models;

namespace EventInvitationWebApp.Services.Interface
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User applicationUser, IEnumerable<string> roles);
    }
}
