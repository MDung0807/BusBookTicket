using BusBookTicket.AddressManagement.Services;
using BusBookTicket.AddressManagement.Services.DistrictService;
using BusBookTicket.AddressManagement.Services.ProvinceService;
using BusBookTicket.AddressManagement.Services.RegionService;
using BusBookTicket.AddressManagement.Services.UnitService;
using BusBookTicket.AddressManagement.Services.WardService;
using BusBookTicket.Application.CloudImage;
using BusBookTicket.Application.CloudImage.Repositories;
using BusBookTicket.Application.CloudImage.Services;
using BusBookTicket.Application.MailKet.Service;
using BusBookTicket.Application.OTP.Services;
using BusBookTicket.Auth.Services.AuthService;
using BusBookTicket.Auth.Services.RoleService;
using BusBookTicket.Buses.Services.BusTypeServices;
using BusBookTicket.BusStationManage.Services;
using BusBookTicket.CompanyManage.Services;
using BusBookTicket.CustomerManage.Services;
using BusBookTicket.DiscountManage.Services;
using BusBookTicket.Ranks.Services;
using BusBookTicket.BillManage.Services.BillItems;
using BusBookTicket.BillManage.Services.Bills;
using BusBookTicket.Buses.Services.BusServices;
using BusBookTicket.Buses.Services.SeatServices;
using BusBookTicket.Buses.Services.SeatTypServices;
using BusBookTicket.Core.Infrastructure;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Ticket.Services.TicketItemServices;
using BusBookTicket.Ticket.Services.TicketServices;

namespace BusBookTicket.Configs
{
    public static class ScopedConfigs
    {
        public static void Configure(IServiceCollection services)
        {
            #region -- Add Scoped Customer Module --
            services.AddScoped<ICustomerService, CustomerService>();
            #endregion -- Add Scoped Customer Module --
            
            #region -- Add Scoped Auth Module --
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleService, RoleService>();
            #endregion -- Add Scoped Auth Module --
            
            #region -- Add Scoped BusStation Module --
            services.AddScoped<IBusStationService, BusStationService>();
            #endregion -- Add BusStation Auth Module --
            
            #region -- Add Scoped Company Module --
            services.AddScoped<ICompanyServices, CompanyService>();
            #endregion -- Add Scoped Company Module --

            #region -- Add Scoped Ranks Module --
            services.AddScoped<IRankService, RankService>();
            #endregion
            
            #region -- Add Scoped Discount Module --
            services.AddScoped<IDiscountService, DiscountService>();
            #endregion -- Add Scoped Discount Module --

            #region  -- Add Scoped Bill Module --
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IBillItemService, BillItemService>();

            #endregion -- Add Scoped Bill Module --

            #region -- Add Scoped Application Service --
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IOtpService, OtpService>();
            #endregion -- Add Scoped Application Service --

            #region -- Add Scroped Buses Module --

            services.AddScoped<IBusTypeService, BusTypeService>();
            
            services.AddScoped<IBusService, BusService>();

            services.AddScoped<ISeatService, SeatService>();

            services.AddScoped<ISeatTypeService, SeatTypeService>();

            #endregion -- Add Scroped Buses Module --

            #region -- Add Scoped Ticket Module --

            services.AddScoped<ITicketService, TicketService>();

            services.AddScoped<ITicketItemService, TicketItemService>();

            #endregion -- Add Scoped Ticket Module --

            #region -- Add Scoped ImageCloudDianary --

            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IImageRepository, ImageRepository>();

            services.AddSingleton(typeof(ClouImageCore));

            #endregion -- Add Scoped ImageCloudDianary --

            #region -- Add Scoped UnitWork --

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion -- Add Scoped UnitWork --

            #region -- Add Scoped Core --


            #endregion -- Add Scoped Core --

            #region -- Add Scoped Address --

            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<IProvinceService, ProvinceService>();
            services.AddScoped<IDistrictService, DistrictService>();
            services.AddScoped<IWardService, WardService>();

            #endregion -- Add Scoped Address --
        }
    }
}
