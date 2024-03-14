using EventInvitationWebApp.Utilities.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventInvitationWebApp.Models
{
    public class Invitation
    {
        [Key]
        public string Id { get; set; }

        public string EventId { get; set; }
        [JsonIgnore]
        public virtual Event Event { get; set; }

        public string UserId { get; set; }
      
        public virtual User InvitedUser { get; set; }

        public InvitationStatus Response { get; set; } = InvitationStatus.pending;
    }
}
