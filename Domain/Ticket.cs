using System;

namespace Domain
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime DateFirst { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateDeadline { get; set; }
    }
}