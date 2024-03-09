using System.ComponentModel.DataAnnotations;

namespace EventInvitationWebApp.Models.ViewModel
{
    public class EventViewModel
    {
       
        public string Name { get; set; }
       
        public DateTime StartDate { get; set; }
      
        public DateTime EndDate { get; set; }

        public string CreatorId { get; set; }
    }
}
