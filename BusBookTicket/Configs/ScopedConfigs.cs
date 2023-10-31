﻿using BusBookTicket.Application.CloudImage;
using BusBookTicket.Application.CloudImage.Repositories;
using BusBookTicket.Application.CloudImage.Services;
using BusBookTicket.Application.MailKet.Service;
using BusBookTicket.Auth.Repositories.AuthRepository;
using BusBookTicket.Auth.Repositories.RoleRepository;
using BusBookTicket.Auth.Services.AuthService;
using BusBookTicket.Auth.Services.RoleService;
using BusBookTicket.Buses.Repositories.BusTypeRepositories;
using BusBookTicket.Buses.Services.BusTypeServices;
using BusBookTicket.BusStationManage.Repositories;
using BusBookTicket.BusStationManage.Services;
using BusBookTicket.CompanyManage.Repositories;
using BusBookTicket.CompanyManage.Services;
using BusBookTicket.CustomerManage.Repositories;
using BusBookTicket.CustomerManage.Services;
using BusBookTicket.DiscountManager.Repositories;
using BusBookTicket.DiscountManager.Services;
using BusBookTicket.Ranks.Repositories;
using BusBookTicket.Ranks.Services;
using BusBookTicket.BillManage.Repositories.BillItems;
using BusBookTicket.BillManage.Repositories.Bills;
using BusBookTicket.BillManage.Services.BillItems;
using BusBookTicket.BillManage.Services.Bills;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Buses.Repositories.SeatRepositories;
using BusBookTicket.Buses.Repositories.SeatTypeRepositories;
using BusBookTicket.Buses.Services.SeatServices;
using BusBookTicket.Buses.Services.SeatTypServices;
using BusBookTicket.Core.Common;
using BusBookTicket.Ticket.Responses.TicketItemRespositories;
using BusBookTicket.Ticket.Responses.TicketRepositories;
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
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            #endregion -- Add Scoped Customer Module --
            
            #region -- Add Scoped Auth Module --
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            #endregion -- Add Scoped Auth Module --
            
            #region -- Add Scoped BusStation Module --
            services.AddScoped<IBusStationService, BusStationService>();
            services.AddScoped<IBusStationRepos, BusStationRepos>();
            #endregion -- Add BusStation Auth Module --
            
            #region -- Add Scoped Company Module --
            services.AddScoped<ICompanyRepos, CompanyRepos>();
            services.AddScoped<ICompanyServices, CompanyService>();
            #endregion -- Add Scoped Company Module --

            #region -- Add Scoped Ranks Module --
            services.AddScoped<IRankService, RankService>();
            services.AddScoped<IRankRepository, RankRepository>();
            #endregion
            
            #region -- Add Scoped Discount Module --
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            #endregion -- Add Scoped Discount Module --

            #region  -- Add Scoped Bill Module --
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IBillItemService, BillItemService>();
            services.AddScoped<IBillItemRepos, BillItemRepos>();

            #endregion -- Add Scoped Bill Module --

            #region -- Add Scoped Mail Service --
            services.AddTransient<IMailService, MailService>();

            #endregion -- Add Scoped Mail Service --

            #region -- Add Scroped Buses Module --

            services.AddScoped<IBusTypeRepos, BusTypeRepos>();
            services.AddScoped<IBusTypeService, BusTypeService>();
            
            services.AddScoped<IBusService, BusService>();
            services.AddScoped<IBusRepos, BusRepos>();

            services.AddScoped<ISeatService, SeatService>();
            services.AddScoped<ISeatRepository, SeatRepository>();

            services.AddScoped<ISeatTypeService, SeatTypeService>();
            services.AddScoped<ISeatTypeRepos, SeatTypeRepos>();

            #endregion -- Add Scroped Buses Module --

            #region -- Add Scoped Ticket Module --

            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<ITicketRepository, TicketRepository>();

            services.AddScoped<ITicketItemService, TicketItemService>();
            services.AddScoped<ITicketItemRepos, TicketItemRepos>();

            #endregion -- Add Scoped Ticket Module --

            #region -- Add Scoped ImageCloudDianary --

            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IImageRepository, ImageRepository>();

            services.AddSingleton(typeof(ClouImageCore));

            #endregion -- Add Scoped ImageCloudDianary --

            #region -- Add Scoped UnitWork --

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion -- Add Scoped UnitWork --
        }
    }
}
