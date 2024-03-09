using EventInvitationWebApp.Models;
using EventInvitationWebApp.Models.DTO;

namespace EventInvitationWebApp.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User> GetUserById(string  userId);
        Task<User> GetUserByName(string name);
      
    }
}
