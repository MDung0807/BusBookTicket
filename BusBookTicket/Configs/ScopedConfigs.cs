using BusBookTicket.Auth.Repositories.AuthRepository;
using BusBookTicket.Auth.Repositories.RoleRepository;
using BusBookTicket.Auth.Services.AuthService;
using BusBookTicket.Auth.Services.RoleService;
using BusBookTicket.BusStationManage.Repositories;
using BusBookTicket.BusStationManage.Services;
using BusBookTicket.Common.Models.Entity;
using BusBookTicket.CustomerManage.Repositories;
using BusBookTicket.CustomerManage.Services;

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
            
            #region -- Add Scoped Auth Module --
            services.AddScoped<IBusStationService, BusStationService>();
            services.AddScoped<IBusStationRepos, BusStationRepos>();
            #endregion -- Add Scoped Auth Module --
        }
    }
}
