using System.Linq;
using Application.Tickets.Dtos;
using AutoMapper;
using Domain;

namespace Application.Tickets.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Ticket, TicketDto>();
            CreateMap<UserTicket, AttendeeDto>()
                .ForMember(d => d.Username, o => o.MapFrom(s => s.AppUser))
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.AppUser.DisplayName))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.AppUser.Photos.FirstOrDefault(
                    x => x.IsMain
                ).Url))
                .ForMember(d => d.Following, o => o.MapFrom<FollowingResolver>());

        }
    }
}