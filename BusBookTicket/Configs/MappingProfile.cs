using AutoMapper;
using BusBookTicket.Common.Models.Entity;
using BusBookTicket.CustomerManage.DTOs.Requests;

namespace BusBookTicket.Configs
{
    public class MappingProfile : Profile
    {
        public MappingProfile() { 
            CreateMap<Customer, FormRegister>();
            CreateMap<FormRegister, Customer> ();
        }
    }
}
