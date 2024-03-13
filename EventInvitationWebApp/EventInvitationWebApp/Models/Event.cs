using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace EventInvitationWebApp.Models
{
    public class Event
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public User Creator { get; set; }
        public List<User> InvitedUsers { get; } = new List<User>();
        [JsonIgnore]
        public virtual List<Invitation> Invitations { get; } = new List<Invitation>();
    }
}
