namespace EventInvitationWebApp.Models.DTO
{
    public class EventDto
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public UserDto Creator { get; set; }
    }
}
