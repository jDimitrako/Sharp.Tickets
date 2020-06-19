using System;

namespace Domain
{
    public class UserTicket
    {
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public Guid TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
        public DateTime DateJoined { get; set; }
        public bool IsHost { get; set; }
    }
}