using EventInvitationWebApp.Utilities.Enums;

namespace EventInvitationWebApp.Models.DTO
{
    public class InviteResponseDto
    {
        public string EventId { get; set; }
        public string UserId { get; set; }
        public string RespondingUserName { get; set; }
        public string Status { get; set; }
        //public InvitationStatus Response { get; set; }


        public InviteResponseDto(string eventId, string userId, string respondingUserName, InvitationStatus response)
        {
            EventId = eventId;
            UserId = userId; 
            RespondingUserName = respondingUserName;
            Status = response.ToString(); // Convert enum value to string
        }
    }

   
}
