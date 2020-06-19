using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Tickets.Dtos;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Tickets
{
    public class Details
    {
        public class Query : IRequest<TicketDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, TicketDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<TicketDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var ticket = await _context.Tickets
                    .FindAsync(request.Id);

                if (ticket == null)
                    throw new RestException(HttpStatusCode.NotFound, new
                    { ticket = "Not Found" });

                var ticketToReturn = _mapper.Map<Ticket, TicketDto>(ticket);

                return ticketToReturn;
            }
        }
    }
}