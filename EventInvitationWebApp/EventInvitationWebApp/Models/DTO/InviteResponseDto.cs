using EventInvitationWebApp.Utilities.Enums;

namespace EventInvitationWebApp.Models.DTO
{
    public class InviteResponseDto
    {
        public string EventId { get; set; }
        public string RespondingUserName { get; set; }
        public string Status { get; set; }
        //public InvitationStatus Response { get; set; }


        public InviteResponseDto(string eventId, string respondingUserName, InvitationStatus response)
        {
            EventId = eventId;
            RespondingUserName = respondingUserName;
            Status = response.ToString(); // Convert enum value to string
        }
    }

   
}
