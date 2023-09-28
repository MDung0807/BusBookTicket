using BusBookTicket.Auth.Services.AuthService;
using BusBookTicket.Common.Models.Entity;
using BusBookTicket.CustomerManage.Repositories;
using BusBookTicket.CustomerManage.Services;

namespace BusBookTicket.Configs
{
    public static class ScopedConfigs
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<Customer>();
        }
    }
}
