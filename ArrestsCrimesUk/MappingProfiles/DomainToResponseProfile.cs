using Entities;
using ArrestsCrimesUk.Contracts.Responses;
using AutoMapper;

namespace ArrestsCrimesUk.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Arrest, ArrestsResponse>();
            CreateMap<Victim, VictimsResponse>();
        }
    }
}
