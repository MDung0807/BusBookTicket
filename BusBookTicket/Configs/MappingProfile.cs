using AutoMapper;
using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Common.Models.Entity;
using BusBookTicket.CustomerManage.DTOs.Requests;
using BusBookTicket.CustomerManage.DTOs.Responses;

namespace BusBookTicket.Configs
{
    public class MappingProfile : Profile
    {
        public MappingProfile() { 
            CreateMap<FormRegister, Customer> ();
            CreateMap<FormRegister, AuthRequest>();
            CreateMap<Customer, ProfileResponse>()
                .ForPath(dest => dest.roleName,
                    opt => opt.MapFrom(x => x.account.role.roleName))
                .ForPath(dest => dest.username, 
                    opt => opt.MapFrom(x => x.account.username))
                .ForPath(dest => dest.rank,
                    opt => opt.MapFrom(x => x.rank.name));
            CreateMap<Customer, CustomerResponse>()
                .ForPath(dest => dest.username, 
                    opt => opt.MapFrom(x => x.account.username))
                .ForPath(dest => dest.rank,
                    opt => opt.MapFrom(x => x.rank.name));
            CreateMap<AuthRequest, Account>()
                .ForPath(dest => dest.role.roleName, 
                    opt => opt.MapFrom(x => x.roleName));

        }
    }
}
