using EventInvitationWebApp.Data;
using EventInvitationWebApp.Models;
using EventInvitationWebApp.Repositories.Interface;
using EventInvitationWebApp.Utilities.Enums;
using Microsoft.EntityFrameworkCore;

namespace EventInvitationWebApp.Repositories.Implementation
{
    public class EventRepository : IEventRepository
    {
        private readonly EventInvitationDbContext _eventInvitationDbContext;

        public EventRepository(EventInvitationDbContext eventInvitationDbContext)
        {
            this._eventInvitationDbContext = eventInvitationDbContext;
        }

        public async Task<Event> CreateEventAsync(Event newEvent)
        {
            _eventInvitationDbContext.Events.Add(newEvent);
            await _eventInvitationDbContext.SaveChangesAsync();

            return newEvent;
        }

        public async Task<Event> UpdateEventAsync(Event newEvent)
        {
            _eventInvitationDbContext.Events.Update(newEvent);
            await _eventInvitationDbContext.SaveChangesAsync();

            return newEvent;

        }

        public async Task<Event> GetEventById(string eventId)
        {
            return await _eventInvitationDbContext.Events
                            .Include(e => e.Creator)
                            .Include(e => e.Invitations)
                            .ThenInclude(i => i.InvitedUser) // Include User in the Invitations collection
                            .FirstOrDefaultAsync(e => e.Id == eventId);


        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            return await _eventInvitationDbContext.Events.Include(e => e.Creator)
                .Include(i => i.Invitations)
                .ToListAsync();
        }

        public async Task<List<Event>> GetEventsByUserAsync(string userId)
        {
            return await _eventInvitationDbContext.Events.Where(e => e.Creator.Id == userId).ToListAsync();
        }

        public async Task<List<Event>> GetEventsUserIsInvitedTo(string userId)
        {
            return await _eventInvitationDbContext.Events
                .Include(e => e.Creator)
                .Include (i => i.Invitations)
                .Where(e => e.Invitations.Any(i => i.UserId == userId && i.Response == InvitationStatus.pending))
                .ToListAsync();
                
        }

        public async Task<Invitation> GetInviteResponse(string userId, string eventId)
        {
            return await _eventInvitationDbContext.Invitations
                .FirstOrDefaultAsync(i => i.UserId == userId && i.EventId == eventId);
        }

    }
}
