using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;
using FluentValidation;

namespace Application.Tickets
{
    public class Create
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Category { get; set; }
            public DateTime DateFirst { get; set; }
            public DateTime DateModified { get; set; }
            public DateTime DateDeadline { get; set; }
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
                var ticket = new Ticket 
                {
                    Id = request.Id,
                    Title = request.Title,
                    Description = request.Description,
                    Category = request.Category,
                    DateFirst = request.DateFirst,
                    DateModified = request.DateModified,
                    DateDeadline = request.DateDeadline
                };

                _context.Add(ticket);
                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}