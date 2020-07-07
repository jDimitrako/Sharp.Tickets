using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Profiles;
using Application.Tickets.Profiles;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProfilesController : BaseController
    {
        [HttpGet("{username}")]
        public async Task<ActionResult<Profile>> Get(string username)
        {
            return await Mediator.Send(new Details.Query { Username = username });
        }

        [HttpGet("{username}/tickets")]
        public async Task<ActionResult<List<UserTicketDto>>> GetUserTickets(string username, string predicate)
        {
            return await Mediator.Send(new ListTickets.Query { Username = username, Predicate = predicate });
        }
    }
}