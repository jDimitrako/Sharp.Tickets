using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Tickets
{
    public class Unattend
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                //handler logic
                var ticket = await _context.Tickets.FindAsync(request.Id);

                if (ticket == null)
                    throw new RestException(HttpStatusCode.NotFound, new { ticket = "not Found" });

                var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == _userAccessor.GetCurrentUsername());

                var attendance = await _context.UserTickets.SingleOrDefaultAsync(x => x.TicketId == ticket.Id &&
                 x.AppUserId == user.Id);

                if (attendance == null)
                    throw new RestException(HttpStatusCode.BadRequest, new { attendance = "You are not attending in this ticket" });

                if (attendance.IsHost)
                    throw new RestException(HttpStatusCode.BadRequest, new { attendance = "You are hosting the ticket, you cant unregister" });

                _context.UserTickets.Remove(attendance);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving Changes");
            }
        }
    }
}