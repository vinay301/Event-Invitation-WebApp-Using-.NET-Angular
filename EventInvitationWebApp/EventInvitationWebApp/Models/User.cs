using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventInvitationWebApp.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

     
        public virtual ICollection<Invitation> ReceivedInvitations { get; set; }
    }
}
