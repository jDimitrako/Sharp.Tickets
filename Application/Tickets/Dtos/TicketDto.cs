using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Tickets.Dtos
{
    public class TicketDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime DateFirst { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateDeadline { get; set; }
        [JsonPropertyName("attendees")]
        public ICollection<AttendeeDto> UserTickets { get; set; }
    }
}