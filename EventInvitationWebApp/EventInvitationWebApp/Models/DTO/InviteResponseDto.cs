using EventInvitationWebApp.Utilities.Enums;

namespace EventInvitationWebApp.Models.DTO
{
    public class InviteResponseDto
    {
        public string EventId { get; set; }
        public string RespondingUserName { get; set; }
        public string Status { get; set; }
        //public InvitationStatus Response { get; set; }
    }
}
