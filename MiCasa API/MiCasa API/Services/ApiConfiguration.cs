﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using System.Net;
using System.Text;

namespace MiCasa.Services
{

    /// <summary>
    /// Makes most of the required configuration using <paramref name="IServiceCollection">
    /// </summary>
    public class ApiConfiguration
    {
        private readonly IServiceCollection? _services;
        private readonly IConfiguration? _configuration;

        private string GetLocaleIpAdresse()
        {
            string hostName = Dns.GetHostName();
            return Dns.GetHostEntry(hostName).AddressList[0].ToString();
        }

        public ApiConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;
        }


        /// <summary>
        /// JSON responses configuration. <br/>
        /// With this configuration, we ensure that every given response by the API will be in JSON format.<br/>
        /// Using the AddControllersWithViews allows OpenApi and the whole program to add services for pages.
        /// </summary>
        public ApiConfiguration ConfigureJsonSerialization()
        {
            _services?.AddControllersWithViews().AddNewtonsoftJson(opt =>
                opt.SerializerSettings.ContractResolver = new DefaultContractResolver()).AddNewtonsoftJson(opt =>
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize);

            return this;
        }

        /// <summary>
        /// Enabling CORS for services
        /// </summary>
        public ApiConfiguration ConfigureCors()
        {
            _services?.AddCors(options =>
                options.AddPolicy(name: "AllowOrigin", opt =>
                {
                    opt.WithOrigins("http://localhost:4200",
                        "https://inquisitive-snickerdoodle-f9acf7.netlify.app",
                        "http://localhost:8080",
                        GetLocaleIpAdresse()
                        )
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                }));

            return this;
        }

        /// <summary>
        /// Registering services in the DI container
        /// </summary>
        public ApiConfiguration ConfigureInterfacesRegistration()
        {
            _services?.AddScoped<IAgence, BLL_Agence>()
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<IContratAgence, BLL_ContratAgence>()
                .AddScoped<IAdministrateur, BLL_Administrateur>()
                .AddScoped<IContratClient, BllContratClient>();

            return this;
        }

        public ApiConfiguration ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(
                    path: Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                        "MiCasa Logs",
                        "log-.txt"),
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: LogEventLevel.Information).CreateLogger();

            return this;
        }

        /// <summary>
        /// Registers <see cref="IAuthService"/> in the DI container so that it can be used.
        /// </summary>
        /// <returns></returns>
        public ApiConfiguration ConfigureEmailService()
        {
            _services!.AddScoped<IAuthService, AuthService>();

            return this;
        }

        /// <summary>
        /// Registers <see cref="AppDbContext"/> as the database context.
        /// </summary>
        /// <returns></returns>
        public ApiConfiguration ConfigureDbContext()
        {
            _services?.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(_configuration.GetConnectionString("MiCasaDB")));

            return this;
        }

        /// <summary>
        /// Configures the JWT generation in order to protect ressources
        /// </summary>
        /// <returns></returns>
        public ApiConfiguration ConfigureJwt()
        {
            _services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    // The token is going to be valid if =>
                    options.TokenValidationParameters = new TokenValidationParameters {
                        // The issuer is the actual server that created the server
                        ValidateIssuer = true,

                        // The receiver of the token is a valid recipient
                        ValidateAudience = true,

                        // The token has not expired
                        ValidateLifetime = true,
                        // The signin key is valid and trusted by the server

                        ValidIssuer = "http://localhost:5000",
                        ValidAudience = "http://localhost:5000",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                            _configuration.GetValue<string>("JwtKey")))
                    };
                });


            return this;
        }
    }
}