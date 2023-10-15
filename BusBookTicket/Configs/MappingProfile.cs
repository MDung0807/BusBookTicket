using AutoMapper;
using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.BusStationManage.DTOs.Requests;
using BusBookTicket.BusStationManage.DTOs.Responses;
using BusBookTicket.Common.Models.Entity;
using BusBookTicket.CompanyManage.DTOs.Requests;
using BusBookTicket.CompanyManage.DTOs.Responses;
using BusBookTicket.CustomerManage.DTOs.Requests;
using BusBookTicket.CustomerManage.DTOs.Responses;
using BusBookTicket.Ranks.DTOs.Requests;
using BusBookTicket.Ranks.DTOs.Responses;

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
            
            CreateMap<FormRegisterCompany, AuthRequest>();
            #endregion -- Configs Auth Module --

            #region -- Configs BusStation Module --
            CreateMap<BST_FormUpdate, BusStation>();
            CreateMap<BST_FormCreate, BusStation>();
            CreateMap<BusStation, BusStationResponse>();
            #endregion -- Configs BusStation Module --

            #region -- Configs Company Module --
            CreateMap<FormRegisterCompany, Company>();
            CreateMap<FormUpdateCompany, Company>();
            CreateMap<Company, ProfileCompany>()
                .ForPath(dest => dest.roleName,
                    opt => opt.MapFrom(x => x.account.role.roleName))
                .ForPath(dest => dest.username,
                    opt => opt.MapFrom(x => x.account.username));
            #endregion -- Configs Company Module --

            #region -- Configs Ranks Module --
            CreateMap<RankCreate, Rank>();
            CreateMap<RankUpdate, Rank>();
            CreateMap<Rank, RankResponse>();
            #endregion -- Configs Ranks Module --
        }
    }
}
