using AutoMapper;
using SCEAPI.Models;
using SCEAPI.Models.DTOs;

namespace SCEAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Event, EventDTO>().ReverseMap();

            CreateMap<Event, EventCreateDTO>().ReverseMap();
            CreateMap<Event, EventUpdateDTO>().ReverseMap();
        }
    }
}