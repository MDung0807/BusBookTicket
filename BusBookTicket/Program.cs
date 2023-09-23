using AutoMapper;
using BusBookTicket.Auth.Controllers;
using BusBookTicket.Auth.Services;
using BusBookTicket.Common.Common;
using BusBookTicket.Common.Models.Entity;
using BusBookTicket.Common.Models.EntityFW;
using BusBookTicket.Configs;
using BusBookTicket.CustomerManage.DTOs.Requests;
using BusBookTicket.CustomerManage.Repositories;
using BusBookTicket.CustomerManage.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;

        #region -- Config auto mapping --
        var mapperConfigs = new MapperConfiguration(cfg =>
        cfg.AddProfile(new MappingProfile())
      );
        IMapper mapper = mapperConfigs.CreateMapper();
        services.AddSingleton(mapper);
        #endregion -- Config auto mapping --



        // Add services to the container.
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.AddDbContext<AppDBContext>(
            options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDB")));
        services.AddSwaggerGen();

        #region -- Scoped --
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<Customer>();
        #endregion -- Scoped --

        var app = builder.Build();

        // Configure the HTTP request pipeline.


        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        endpoints.MapControllers()) ;
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });

        app.Run();
    }
}