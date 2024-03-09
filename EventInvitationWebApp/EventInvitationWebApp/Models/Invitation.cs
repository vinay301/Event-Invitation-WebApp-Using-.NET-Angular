using EventInvitationWebApp.Utilities.Enums;
using System.ComponentModel.DataAnnotations;

namespace EventInvitationWebApp.Models
{
    public class Invitation
    {
        [Key]
        public string Id { get; set; }

        public string EventId { get; set; }
        public Event Event { get; set; }

        public string UserId { get; set; }
        public User InvitedUser { get; set; }

        public InvitationStatus Response { get; set; } = InvitationStatus.Pending;
    }
}
