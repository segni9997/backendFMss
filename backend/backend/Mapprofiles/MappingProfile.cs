using AutoMapper;
using backend.Dto;
using backend.Models;

namespace backend.Mapprofiles
{
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<Cabincrew , CabinCrewDto>();
            CreateMap<CabinCrewDto, Cabincrew>();
            CreateMap<CoPilot , CoPilotDto>();
            CreateMap<CoPilotDto, CoPilot>();
            CreateMap<TechncianDto, Technician>();
            CreateMap<Technician, TechncianDto>();
            CreateMap<Pilot , PilotDto>();
            CreateMap< PilotDto, Pilot>();
            CreateMap<Flight , FlightDto>();
            CreateMap<FlightDto,Flight >();
            CreateMap<AirCraftDto, Aircraft>();
            CreateMap< Aircraft, AirCraftDto>();    
            CreateMap<Admin, AdminDto>();
            CreateMap<AdminDto, Admin>();
            CreateMap<Tresury, TreasuryDto>();  
            CreateMap<TreasuryDto,Tresury >();  
            CreateMap <User , UserDto>();
            CreateMap <UserDto , User>();
            CreateMap<User, UserRegisterDto>();
            CreateMap<UserRegisterDto, User>();
            CreateMap<User, PasswordDto>();
            CreateMap<PasswordDto, User>();






        }
    }
}
