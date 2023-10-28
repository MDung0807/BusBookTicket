using System.Runtime.ConstrainedExecution;
using AutoMapper;
using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Auth.DTOs.Responses;
using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.BusStationManage.DTOs.Requests;
using BusBookTicket.BusStationManage.DTOs.Responses;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.CompanyManage.DTOs.Requests;
using BusBookTicket.CompanyManage.DTOs.Responses;
using BusBookTicket.CustomerManage.DTOs.Requests;
using BusBookTicket.CustomerManage.DTOs.Responses;
using BusBookTicket.DiscountManager.DTOs.Requests;
using BusBookTicket.DiscountManager.DTOs.Responses;
using BusBookTicket.Ranks.DTOs.Requests;
using BusBookTicket.Ranks.DTOs.Responses;
using BusBookTicket.BillManage.DTOs.Requests;
using BusBookTicket.BillManage.DTOs.Responses;
using BusBookTicket.Ticket.DTOs.Requests;
using BusBookTicket.Ticket.DTOs.Response;
using FormUpdate = BusBookTicket.CustomerManage.DTOs.Requests.FormUpdate;

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
            CreateMap<Account, AccResponse>();
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
            
            #region -- Configs Dícounts Module --
            CreateMap<DiscountCreate, Rank>();
            CreateMap<DiscountUpdate, Rank>();
            CreateMap<Rank, DiscountResponse>();
            #endregion -- Configs Dícounts Module --

            #region -- Configs Bill Module --

            CreateMap<BillRequest, Bill>();
            CreateMap<BillItemRequest, BillItem>();
            
            CreateMap<Bill, BillResponse>()
                .ForPath(dest => dest.nameCustomer,
                    opts => opts.MapFrom(x => x.customer.fullName))
                .ForPath(dest => dest.busStationStart,
                    opts => opts.MapFrom(x => x.busStationStart.name))
                .ForPath(dest => dest.busStationEnd,
                    opts => opts.MapFrom(x => x.busStationEnd.name))
                .ForPath(dest => dest.discount,
                    opts => opts.MapFrom(x => x.discount.name));

            CreateMap<BillItem, BillItemResponse>()
                .ForPath(dest => dest.company,
                    memberOptions: opts => opts.MapFrom(x => x.TicketItem.ticket.bus.company.name))
                .ForPath(dest => dest.busNumber,
                    opts => opts.MapFrom(x => x.TicketItem.ticket.bus.busNumber));

            #endregion -- Configs Bill Module --

            #region -- Configs Buses Module --

            // Bus
            CreateMap<FormCreateBus, Bus>()
                .ForPath(dest => dest.busType.name,
                    opts => opts.MapFrom(x => x.busType))
                .ForPath(dest => dest.company.companyID, 
                    opts => opts.MapFrom(x => x.companyID));
            CreateMap<FormUpdateBus, Bus>()
                .ForPath(dest => dest.busType.name,
                    opts => opts.MapFrom(x => x.busType))
                .ForPath(dest => dest.company.companyID, 
                    opts => opts.MapFrom(x => x.companyID));;
            CreateMap<Bus, BusResponse>()
                .ForPath(dest => dest.company,
                    opts => opts.MapFrom(x => x.company.name))
                .ForPath(dest => dest.busType,
                    opts => opts.MapFrom(x => x.busType.name));

            CreateMap<BusTypeForm, BusType>();
            CreateMap<BusTypeFormUpdate, BusType>();
            CreateMap<BusType, BusTypeResponse>();

            //Seat
            CreateMap<SeatTypeFormCreate, SeatType>()
                .ForPath(dest => dest.Company.companyID,
                    opts => opts.MapFrom(x => x.companyID));

            CreateMap<SeatTypeFormUpdate, SeatType>()
                .ForPath(dest => dest.Company.companyID,
                    opts => opts.MapFrom(x => x.companyID));

            CreateMap<SeatType, SeatTypeResponse>();

            CreateMap<SeatForm, Seat>()
                .ForPath(dest => dest.bus.busID,
                    opts => opts.MapFrom(x => x.busID));

            CreateMap<SeatForm, Seat>()
                .ForPath(dest => dest.seatType.typeID,
                    opts => opts.MapFrom(x => x.typeID));
            #endregion -- Configs Buses Module --

            #region -- Configs Ticket Module --

            //Ticket
            CreateMap<TicketFormCreate, Core.Models.Entity.Ticket>()
                .ForPath(dest => dest.bus.busID,
                    opts => opts.MapFrom(x => x.busID));
            
            CreateMap<TicketFormUpdate, Core.Models.Entity.Ticket>()
                .ForPath(dest => dest.bus.busID,
                    opts => opts.MapFrom(x => x.busID));

            CreateMap<Core.Models.Entity.Ticket, TicketResponse>()
                .ForPath(dest => dest.busNumber,
                    opts => opts.MapFrom(x => x.bus.busNumber))
                .ForPath(dest => dest.company,
                    opts => opts.MapFrom(x => x.bus.company.name));

            //TicketItem
            CreateMap<TicketItemForm, TicketItem>();
            CreateMap<TicketItem, TicketItemResponse>();

            #endregion -- Configs Ticket Module --
        }
    }
}
