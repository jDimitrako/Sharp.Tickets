using System;

namespace Domain
{
    public class UserTicket
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public DateTime DateJoined { get; set; }
        public bool IsHost { get; set; }
    }
}