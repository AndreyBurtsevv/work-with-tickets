using AutoMapper;
using BoxOffice.Core.Data.Entities;
using BoxOffice.Core.Dto;

namespace BoxOffice.Core.Data.Mapper
{
    public class MappingEntity : Profile
    {
        public MappingEntity()
        {
            CreateMap<Spectacle, SpectacleDto>().ReverseMap();
            CreateMap<Spectacle, CreateSpectacle>().ReverseMap();

            CreateMap<Admin, Registration>().ReverseMap();
            CreateMap<Admin, AdminDto>().ReverseMap();

            CreateMap<Client, Registration>().ReverseMap();
            CreateMap<Client, ClientDto>().ReverseMap();

            //CreateMap<Ticket, TicketDto>()
            //    .ForMember(x => x.ClientFullName, opt => opt.MapFrom(src => $"{src.Client.FirstName} {src.Client.LastName}"))
            //    .ForMember(x => x.SpectacleName, opt => opt.MapFrom(src => src.Spectacle.Name))
            //    .ForMember(x => x.SpectacleStartTime, opt => opt.MapFrom(src => src.Spectacle.StartTime))
            //    .ForMember(x => x.SpectacleEndTime, opt => opt.MapFrom(src => src.Spectacle.EndTime));

            CreateMap<Ticket, TicketDto>()
                .ForMember(dto => dto.ClientFullName, conf => conf.MapFrom(src => $"{src.Client.FirstName} {src.Client.LastName}"))
                .ForMember(dto => dto.SpectacleName, conf => conf.MapFrom(src => src.Spectacle.Name))
                .ForMember(dto => dto.SpectacleStartTime, conf => conf.MapFrom(src => src.Spectacle.StartTime))
                .ForMember(dto => dto.SpectacleEndTime, conf => conf.MapFrom(src => src.Spectacle.EndTime))
                ;
        }
    }
}
