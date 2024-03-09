using EventInvitationWebApp.Data;
using EventInvitationWebApp.Models;
using EventInvitationWebApp.Models.DTO;
using EventInvitationWebApp.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventInvitationWebApp.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly EventInvitationDbContext _eventInvitationDbContext;

        public UserRepository(EventInvitationDbContext eventInvitationDbContext)
        {
            this._eventInvitationDbContext = eventInvitationDbContext;
        }

        public async Task<User> GetUserById(string userId)
        {
            return await _eventInvitationDbContext.Users.FindAsync(userId);
        }

        public async Task<User> GetUserByName(string name)
        {
            return await _eventInvitationDbContext.Users.FirstAsync(x => x.UserName == name);
        }

      
    }
}
