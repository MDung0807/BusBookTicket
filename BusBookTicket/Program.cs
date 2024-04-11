using AutoMapper;
using BusBookTicket.Application.MailKet.Settings;
using BusBookTicket.Application.Notification;
using BusBookTicket.Application.PayPalPayment.Services;
using BusBookTicket.Configs;
using BusBookTicket.Core.Models.EntityFW;
using BusBookTicket.CustomerManage.DTOs.Requests;
using BusBookTicket.Exceptions;
using BusBookTicket.Ticket.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace BusBookTicket;

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

        #region -- Authen --
        JwtMiddleware.ConfigureService(services);
        #endregion -- Authen --
        
        services.AddAuthorization();
        services.AddControllers();
        services.AddSignalR();
        services.AddEndpointsApiExplorer();

        services.AddDbContext<AppDBContext>(
            options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDB")));
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });


        #region -- Scoped --
        ScopedConfigs.Configure(services: services);

        #endregion -- Scoped --

        services.AddScoped<FormRegister>();
        services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

        services.AddSingleton(
            x => new PaypalClient(
                builder.Configuration["PayPalOptions:ClientId"],
                builder.Configuration["PayPalOptions:ClientSecret"],
                builder.Configuration["PayPalOptions:Mode"]));
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddConsole();
        });
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseRouting();
        app.UseCors(options =>
        {
            options.AllowAnyOrigin();
            options.AllowAnyMethod();
            options.AllowAnyHeader();
        });
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });
        app.MapHub<ReservePlace>("ReservePlace");
        app.MapHub<NotificationHub>("Notification");
        app.Run();
    }
}