using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            public Query(int? limit, int? offset)
            {
                Limit = limit;
                Offset = offset;

            }
            public int? Limit { get; set; }
            public int? Offset { get; set; }
        };

        public class Handler : IRequestHandler<Query, TicketsEnvelope>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<TicketsEnvelope> Handle(Query request, CancellationToken cancellationToken)
            {
                var queryable = _context.Tickets.AsQueryable();

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