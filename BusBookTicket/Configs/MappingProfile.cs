using AutoMapper;
using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.BusStationManage.DTOs.Requests;
using BusBookTicket.BusStationManage.DTOs.Responses;
using BusBookTicket.Common.Models.Entity;
using BusBookTicket.CustomerManage.DTOs.Requests;
using BusBookTicket.CustomerManage.DTOs.Responses;

namespace BusBookTicket.Configs
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            #region -- Configs Customer Module --
            CreateMap<FormRegister, Customer> ();
            CreateMap<FormUpdate, Customer>();
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
            #endregion -- Configs Customer Module --
            
            #region -- Configs Auth Module --
            CreateMap<FormRegister, AuthRequest>();
            CreateMap<AuthRequest, Account>()
                .ForPath(dest => dest.role.roleName, 
                    opt => opt.MapFrom(x => x.roleName));
            
            CreateMap<FormUpdate, Customer>();
            #endregion -- Configs Customer Module --

            #region -- Configs BusStation Module --
            CreateMap<BST_FormUpdate, BusStation>();
            CreateMap<BST_FormCreate, BusStation>();
            CreateMap<BusStation, BusStationResponse>();
            #endregion -- Configs BusStation Module --
        }
    }
}
