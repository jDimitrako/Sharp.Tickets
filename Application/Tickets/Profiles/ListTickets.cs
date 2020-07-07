using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Tickets.Profiles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles
{
    public class ListTickets
    {
        public class Query : IRequest<List<UserTicketDto>>
        {
            public string Username { get; set; }
            public string Predicate { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<UserTicketDto>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<UserTicketDto>> Handle(Query request,
                CancellationToken cancellationToken)
            {
                var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == request.Username);

                if (user == null)
                    throw new RestException(HttpStatusCode.NotFound, new { User = "Not found" });

                var queryable = user.UserTickets
                    .OrderBy(a => a.Ticket.DateFirst)
                    .AsQueryable();

                switch (request.Predicate)
                {
                    case "past":
                        queryable = queryable.Where(a => a.Ticket.DateFirst <= DateTime.Now);
                        break;
                    case "hosting":
                        queryable = queryable.Where(a => a.IsHost);
                        break;
                    default:
                        queryable = queryable.Where(a => a.Ticket.DateFirst >= DateTime.Now);
                        break;
                }

                var tickets = queryable.ToList();
                var ticketsToReturn = new List<UserTicketDto>();

                foreach (var ticket in tickets)
                {
                    var userTicket = new UserTicketDto
                    {
                        Id = ticket.Ticket.Id,
                        Title = ticket.Ticket.Title,
                        Category = ticket.Ticket.Category,
                        Date = ticket.Ticket.DateFirst
                    };

                    ticketsToReturn.Add(userTicket);
                }

                return ticketsToReturn;
            }
        }
    }
}