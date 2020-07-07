using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.interfaces;
using Application.Tickets.Dtos;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Apllication.Tickets
{
    public class List
    {
        public class TicketsEnvelope
        {
            public List<TicketDto> Tickets { get; set; }
            public int TicketCount { get; set; }
        }
        public class Query : IRequest<TicketsEnvelope>
        {
            public Query(int? limit, int? offset, bool isGoing, bool isHost, DateTime?
                startDate)
            {
                Limit = limit;
                Offset = offset;
                IsGoing = isGoing;
                IsHost = isHost;
                StartDate = startDate ?? DateTime.Now.AddMonths(-2);
            }
            public int? Limit { get; set; }
            public int? Offset { get; set; }
            public bool IsGoing { get; set; }
            public bool IsHost { get; set; }
            public DateTime? StartDate { get; set; }
        };

        public class Handler : IRequestHandler<Query, TicketsEnvelope>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _mapper = mapper;
                _context = context;
            }

            public async Task<TicketsEnvelope> Handle(Query request, CancellationToken cancellationToken)
            {
                var queryable = _context.Tickets
                    .Where(x => x.DateFirst >= request.StartDate)
                    .OrderBy(x => x.DateFirst)
                    .AsQueryable();

                if (request.IsGoing && !request.IsHost)
                {
                    queryable = queryable.Where(x => x.UserTickets
                        .Any(a => a.AppUser.UserName == _userAccessor.GetCurrentUsername()));
                }

                if (request.IsHost && !request.IsGoing)
                {
                    queryable = queryable.Where(x => x.UserTickets
                            .Any(a => a.AppUser.UserName == _userAccessor.GetCurrentUsername()
                            && a.IsHost));
                }

                var tickets = await queryable
                    .Skip(request.Offset ?? 0)
                    .Take(request.Limit ?? 3).ToListAsync();

                return new TicketsEnvelope
                {
                    Tickets = _mapper.Map<List<Ticket>, List<TicketDto>>(tickets),
                    TicketCount = queryable.Count()
                };
            }
        }
    }
}