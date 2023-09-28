using AutoMapper;
using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Common.Models.Entity;
using BusBookTicket.CustomerManage.DTOs.Requests;

namespace BusBookTicket.Configs
{
    public class MappingProfile : Profile
    {
        public MappingProfile() { 
            CreateMap<FormRegister, Customer> ();
            CreateMap<FormRegister, AuthRequest>();
            CreateMap<AuthRequest, Account>();
        }
    }
}
