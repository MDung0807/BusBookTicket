using BusBookTicket.Auth.Repositories.AuthRepository;
using BusBookTicket.Auth.Repositories.RoleRepository;
using BusBookTicket.Auth.Services.AuthService;
using BusBookTicket.Auth.Services.RoleService;
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
using BusBookTicket.TicketManage.Repositories.TicketItems;
using BusBookTicket.TicketManage.Repositories.Tickets;
using BusBookTicket.TicketManage.Services.TicketItems;
using BusBookTicket.TicketManage.Services.Tickets;

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

            #region  -- Add Scoped Ticket Module --
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITicketItemService, TicketItemService>();
            services.AddScoped<ITicketItemRepos, TicketItemRepos>();

            #endregion -- Add Scoped Ticket Module --
        }
    }
}
