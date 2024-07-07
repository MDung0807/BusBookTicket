using AutoMapper;
using BusBookTicket.Application.MailKet.Settings;
using BusBookTicket.Application.Notification;
using BusBookTicket.Application.PayPalPayment.Services;
using BusBookTicket.Configs;
using BusBookTicket.Core.Models.EntityFW;
using BusBookTicket.CustomerManage.DTOs.Requests;
using BusBookTicket.Exceptions;
using BusBookTicket.Ticket.Controllers;
using BusBookTicket.Ticket.Services.BackgroundService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
        //
        // builder.WebHost.ConfigureKestrel(options =>
        // {
        //     options.Listen(System.Net.IPAddress.Any, 5107, listenOptions =>
        //     {
        //         listenOptions.UseHttps();
        //     });
        // });

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

        services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
        #region --Background Service --

        // services.AddHostedService<TicketBackgroundService>();

        #endregion --Background Service --
        services.AddSingleton(
            x => new PaypalClient(
                builder.Configuration["PayPalOptions:ClientId"],
                builder.Configuration["PayPalOptions:ClientSecret"],
                builder.Configuration["PayPalOptions:Mode"]));
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddConsole();
        });
        
        //Add service cache in memory
        services.AddMemoryCache();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseRouting();
        app.UseCors(options =>
        {
            options.WithOrigins("http://localhost:3000");
            options.AllowCredentials();
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
        app.MapHub<NotificationHub>("Notification");
        app.Run();
    }
}