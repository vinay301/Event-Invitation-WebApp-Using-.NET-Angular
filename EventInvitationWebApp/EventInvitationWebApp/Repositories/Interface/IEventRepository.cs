using EventInvitationWebApp.Models;
using EventInvitationWebApp.Models.ViewModel;

namespace EventInvitationWebApp.Repositories.Interface
{
    public interface IEventRepository
    {
        Task<Event> CreateEventAsync(Event newEvent);
        Task<Event> UpdateEventAsync(Event newEvent);
        Task<Event> GetEventById(string eventId);

        Task<List<Event>> GetAllEventsAsync();
        //Task DeleteEventAsync(string eventId);
        Task<List<Event>> GetEventsByUserAsync(string userId);
        Task<List<Event>> GetEventsUserIsInvitedTo(string userId);

        Task<Invitation> GetInviteResponse(string userId, string eventId);
    }
}
