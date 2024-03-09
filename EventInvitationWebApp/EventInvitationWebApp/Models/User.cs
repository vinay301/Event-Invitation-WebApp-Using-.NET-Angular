using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EventInvitationWebApp.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public ICollection<Invitation> ReceivedInvitations { get; set; }
    }
}
