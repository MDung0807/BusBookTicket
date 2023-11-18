using AutoMapper;
using BusBookTicket.AddressManagement.DTOs.Requests.District;
using BusBookTicket.AddressManagement.DTOs.Requests.Province;
using BusBookTicket.AddressManagement.DTOs.Requests.Region;
using BusBookTicket.AddressManagement.DTOs.Requests.Unit;
using BusBookTicket.AddressManagement.DTOs.Requests.Ward;
using BusBookTicket.AddressManagement.DTOs.Responses.District;
using BusBookTicket.AddressManagement.DTOs.Responses.Province;
using BusBookTicket.AddressManagement.DTOs.Responses.Region;
using BusBookTicket.AddressManagement.DTOs.Responses.Unit;
using BusBookTicket.AddressManagement.DTOs.Responses.Ward;
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
            CreateMap<FormRegister, Customer> ()
                .ForPath(dest => dest.Ward.Id, 
                    opts => opts.MapFrom(x => x.WardId));
            CreateMap<FormUpdate, Customer>()
                .ForPath(dest => dest.Ward.Id, 
                    opts => opts.MapFrom(x => x.WardId));
            CreateMap<Customer, ProfileResponse>()
                .ForPath(dest => dest.RoleName,
                    opt => opt.MapFrom(x => x.Account.Role.RoleName))
                .ForPath(dest => dest.Username, 
                    opt => opt.MapFrom(x => x.Account.Username))
                .ForPath(dest => dest.Rank,
                    opt => opt.MapFrom(x => x.Rank.Name));
            CreateMap<Customer, CustomerResponse>()
                .ForPath(dest => dest.Username, 
                    opt => opt.MapFrom(x => x.Account.Username))
                .ForPath(dest => dest.Rank,
                    opt => opt.MapFrom(x => x.Rank.Name))
                .ForPath(dest => dest.WardId,
                    opt => opt.MapFrom(x => x.Ward.Id));;
            #endregion -- Configs Customer Module --
            
            #region -- Configs Auth Module --
            CreateMap<FormRegister, AuthRequest>();
            CreateMap<AuthRequest, Account>()
                .ForPath(dest => dest.Role.RoleName, 
                    opt => opt.MapFrom(x => x.RoleName));
            CreateMap<Account, AccResponse>()
                .ForPath(dest => dest.roleName,
                    opts => opts.MapFrom(x => x.Role.RoleName));
            CreateMap<FormRegisterCompany, AuthRequest>();
            #endregion -- Configs Auth Module --

            #region -- Configs BusStation Module --
            CreateMap<BST_FormUpdate, BusStation>()
                .ForPath(dest => dest.Ward.Id, 
                    opts => opts.MapFrom(x => x.WardId));
            CreateMap<BST_FormCreate, BusStation>()
                .ForPath(dest => dest.Ward.Id, 
                    opts => opts.MapFrom(x => x.WardId));
            CreateMap<BusStation, BusStationResponse>()
                .ForPath(dest => dest.WardId, 
                    opts => opts.MapFrom(x => x.Ward.Id));
            #endregion -- Configs BusStation Module --

            #region -- Configs Company Module --
            CreateMap<FormRegisterCompany, Company>()
                .ForPath(dest => dest.Ward.Id,
                    opts =>opts.MapFrom(x => x.WardId));
            CreateMap<FormUpdateCompany, Company>()
                .ForPath(dest => dest.Ward.Id,
                    opts =>opts.MapFrom(x => x.WardId));;
            CreateMap<Company, ProfileCompany>()
                .ForPath(dest => dest.RoleName,
                    opt => opt.MapFrom(x => x.Account.Role.RoleName))
                .ForPath(dest => dest.Username,
                    opt => opt.MapFrom(x => x.Account.Username))
                .ForPath(dest => dest.WardId, 
                    opts => opts.MapFrom(x => x.Ward.Id));
            #endregion -- Configs Company Module --

            #region -- Configs Ranks Module --
            CreateMap<RankCreate, Rank>();
            CreateMap<RankUpdate, Rank>();
            CreateMap<Rank, RankResponse>();
            #endregion -- Configs Ranks Module --
            
            #region -- Configs Dícounts Module --
            CreateMap<DiscountCreate, Discount>()
                .ForPath(dest => dest.Rank.Id,
                    opts => opts.MapFrom(x => x.rankID));
            CreateMap<DiscountUpdate, Discount>()
                .ForPath(dest => dest.Rank.Id,
                    opts => opts.MapFrom(x => x.rankID));;
            CreateMap<Discount, DiscountResponse>()
                .ForPath(dest => dest.rankID,
                    opts => opts.MapFrom(x => x.Rank.Id));;
            #endregion -- Configs Dícounts Module --

            #region -- Configs Bill Module --

            CreateMap<BillRequest, Bill>();
            CreateMap<BillItemRequest, BillItem>();
            
            CreateMap<Bill, BillResponse>()
                .ForPath(dest => dest.nameCustomer,
                    opts => opts.MapFrom(x => x.Customer.FullName))
                .ForPath(dest => dest.busStationStart,
                    opts => opts.MapFrom(x => x.BusStationStart.Name))
                .ForPath(dest => dest.busStationEnd,
                    opts => opts.MapFrom(x => x.BusStationEnd.Name))
                .ForPath(dest => dest.discount,
                    opts => opts.MapFrom(x => x.Discount.Name));

            CreateMap<BillItem, BillItemResponse>()
                .ForPath(dest => dest.company,
                    memberOptions: opts => opts.MapFrom(x => x.TicketItem.Ticket.Bus.Company.Name))
                .ForPath(dest => dest.busNumber,
                    opts => opts.MapFrom(x => x.TicketItem.Ticket.Bus.BusNumber));

            #endregion -- Configs Bill Module --

            #region -- Configs Buses Module --

            // Bus
            CreateMap<FormCreateBus, Bus>()
                .ForPath(dest => dest.BusType.Id,
                    opts => opts.MapFrom(x => x.BusTypeId))
                .ForPath(dest => dest.Company.Id,
                    opts => opts.MapFrom(x => x.CompanyId));
            CreateMap<FormUpdateBus, Bus>()
                .ForPath(dest => dest.BusType.Id,
                        opts => opts.MapFrom(x => x.BusTypeId))
                .ForPath(dest => dest.Company.Id, 
                    opts => opts.MapFrom(x => x.CompanyId));;
            CreateMap<Bus, BusResponse>()
                .ForPath(dest => dest.Company,
                    opts => opts.MapFrom(x => x.Company.Name))
                .ForPath(dest => dest.BusType,
                    opts => opts.MapFrom(x => x.BusType.Name))
                .ForPath(dest => dest.BusStops,opts => opts.MapFrom(x => x.BusStops));

            //BusType
            CreateMap<BusTypeForm, BusType>();
            CreateMap<BusTypeFormUpdate, BusType>();
            CreateMap<BusType, BusTypeResponse>();

            //SeatType
            CreateMap<SeatTypeFormCreate, SeatType>()
                .ForPath(dest => dest.Company.Id,
                    opts => opts.MapFrom(x => x.CompanyId));

            CreateMap<SeatTypeFormUpdate, SeatType>()
                .ForPath(dest => dest.Company.Id,
                    opts => opts.MapFrom(x => x.CompanyId));

            CreateMap<SeatType, SeatTypeResponse>();

            //Seat
            CreateMap<SeatForm, Seat>()
                .ForPath(dest => dest.Bus.Id,
                    opts => opts.MapFrom(x => x.BusId));

            CreateMap<SeatForm, Seat>()
                .ForPath(dest => dest.SeatType.Id,
                    opts => opts.MapFrom(x => x.SeatTypeId));

            #endregion -- Configs Buses Module --

            #region -- Configs Ticket Module --

            //Ticket
            CreateMap<TicketFormCreate, Core.Models.Entity.Ticket>()
                .ForPath(dest => dest.Bus.Id,
                    opts => opts.MapFrom(x => x.busID));
            
            CreateMap<TicketFormUpdate, Core.Models.Entity.Ticket>()
                .ForPath(dest => dest.Bus.Id,
                    opts => opts.MapFrom(x => x.busID));

            CreateMap<Core.Models.Entity.Ticket, TicketResponse>()
                .ForPath(dest => dest.busNumber,
                    opts => opts.MapFrom(x => x.Bus.BusNumber))
                .ForPath(dest => dest.company,
                    opts => opts.MapFrom(x => x.Bus.Company.Name));

            //TicketItem
            CreateMap<TicketItemForm, TicketItem>()
                .ForPath(dest => dest.Ticket.Id,
                    opts => opts.MapFrom(x => x.ticketID));
            CreateMap<TicketItem, TicketItemResponse>();

            #endregion -- Configs Ticket Module --
            
            #region -- Address Module --

            CreateMap<RegionCreate, AdministrativeRegion>();
            CreateMap<RegionUpdate, AdministrativeRegion>();
            CreateMap<AdministrativeRegion, RegionResponse>();

            CreateMap<UnitCreate, AdministrativeUnit>();
            CreateMap<UnitUpdate, AdministrativeUnit>();
            CreateMap<AdministrativeUnit, UnitResponse>();

            CreateMap<ProvinceCreate, Province>();
            CreateMap<ProvinceUpdate, Province>();
            CreateMap<Province, ProvinceResponse>();

            CreateMap<DistrictCreate, District>();
            CreateMap<DistrictUpdate, District>();
            CreateMap<District, DistrictResponse>();

            CreateMap<WardCreate, Ward>();
            CreateMap<WardUpdate, Ward>();
            CreateMap<Ward, WardResponse>()
                .ForPath(dest => dest.District, 
                    opts => opts.MapFrom(x => x.District.FullName))
                .ForPath(dest => dest.Province,
                    opts => opts.MapFrom(x => x.District.Province.FullName));
            #endregion -- Address Module --
        }
    }
}
