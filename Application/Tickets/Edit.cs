using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
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

         public class CommandValidator : AbstractValidator<Command>
        {
             public CommandValidator()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.Category).NotEmpty();
                RuleFor(x => x.DateFirst).NotEmpty();
                RuleFor(x => x.DateModified).NotEmpty();
                RuleFor(x => x.DateDeadline).NotEmpty();
            }
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