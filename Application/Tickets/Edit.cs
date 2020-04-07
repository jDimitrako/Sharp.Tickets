using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.Tickets
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Category { get; set; }
            public DateTime? DateFirst { get; set; }
            public DateTime? DateModified { get; set; }
            public DateTime? DateDeadline { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var ticket = await _context.Tickets.FindAsync(request.Id);

                if (ticket == null)
                    throw new Exception("Could not find ticket");

                ticket.Title = request.Title ?? ticket.Title;
                ticket.Description = request.Description ?? ticket.Description;
                ticket.Category = request.Category ?? ticket.Category; 
                ticket.DateFirst = request.DateFirst ?? ticket.DateFirst;
                ticket.DateModified = request.DateModified ?? ticket.DateModified;
                ticket.DateDeadline = request.DateDeadline ?? ticket.DateDeadline;

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving Changes");
            }
        }
    }
}