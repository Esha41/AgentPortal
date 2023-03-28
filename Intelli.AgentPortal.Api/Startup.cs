using AutoMapper;
using Intelli.AgentPortal.Api.DTO.Mapping;
using Intelli.AgentPortal.Api.Events;
using Intelli.AgentPortal.Api.Helpers;
using Intelli.AgentPortal.Api.Services;
using Intelli.AgentPortal.Api.Services.Batches;
using Intelli.AgentPortal.Api.Services.Batches.Impl;
using Intelli.AgentPortal.Api.Services.BatchStatusService;
using Intelli.AgentPortal.Api.Services.BatchStatusService.Impl;
using Intelli.AgentPortal.Api.Services.BatchVerification;
using Intelli.AgentPortal.Api.Services.BatchVerification.Impl;
using Intelli.AgentPortal.Api.Services.BopConfigService;
using Intelli.AgentPortal.Api.Services.BopConfigService.Impl;
using Intelli.AgentPortal.Api.Services.Cache;
using Intelli.AgentPortal.Api.Services.Cache.Impl;
using Intelli.AgentPortal.Api.Services.Email.Impl;
using Intelli.AgentPortal.Api.Services.Session;
using Intelli.AgentPortal.Api.Services.Session.Impl;
using Intelli.AgentPortal.Domain.Database;
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Domain.Model.Custom;
using Intelli.AgentPortal.EventBus.RabbitMQ.Config;
using Intelli.AgentPortal.EventBus.RabbitMQ.Receiver;
using Intelli.AgentPortal.EventBus.RabbitMQ.Receiver.Impl;
using Intelli.AgentPortal.EventBus.RabbitMQ.Sender;
using Intelli.AgentPortal.EventBus.RabbitMQ.Sender.Impl;
using Intelli.AgentPortal.Helpers;
using Intelli.AgentPortal.Shared;
using Intelli.AgentPortal.Shared.Dto;
using Intelli.AgentPortal.Shared.Mvc.DocumentClassFields;
using Intelli.AgentPortal.Shared.Mvc.Services.CustomerDetailsManager;
using Intelli.AgentPortal.Shared.Mvc.Services.DocumentClassFields.Impl;
using Intelli.AgentPortal.Shared.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;

namespace Intelli.AgentPortal.Api
{
    /// <summary>
    /// The startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets or sets the assembly name.
        /// </summary>
        public string AssemblyName { get; }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure Auto Mapper
            ConfigureAutoMapper(services);

            // Adding MVC API controllers to services
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            // Configure database context
            ConfigureDatabaseContext(services);

            // Configure Authentication
            ConfigureAuthentication(services);

            // Configure audit database context.
            ConfigureAuditDbContext(services);

            // Configure swagger
            ConfigureSwaggerGen(services);

            // Load email sending settings from configuration for the email sender service.
            services.Configure<AuthMessageSenderOptions>(Configuration.GetSection("SendEmailSettings"));

            // Inject email sending service for users' email verification.
            services.AddSingleton<IEmailSender, SmtpEmailSender>();

            // Setting up BopConfigService
            services.AddSingleton<IBopConfigService, BopConfigService>();

            // Setting up BatchStatusesService
            services.AddSingleton<IBatchStatusService, BatchStatusesService>();

            // Setting up DocumentClassFields
            services.AddSingleton<IDocumentClassFields, DocumentClassFields>();

            // Setting up BatchVerificationService
            services.AddScoped<IBatchVerificationService, BatchVerificationService>();

            // Setting up BatchService 
            services.AddScoped<IBatchService, BatchService>();

            // Setting up auth email service
            services.AddSingleton<IAuthEmailService, AuthEmailService>();

            // Setting up DocumentClassFields service

            // Setting up CustomerDetailsManager service
            services.AddScoped<ICustomerDetailsManager, CustomerDetailsManager>();


            // Setting up privileges service
            services.AddScoped<IPrivilegesService, PrivilegesService>();

            // Setting up jwt token service
            services.AddSingleton<IJWTService, JWTService>();

            // Cache settings
            services.Configure<CacheSettings>(Configuration.GetSection(nameof(CacheSettings)));

            // Load email sending settings from configuration for the email sender service.
            services.Configure<SmtpEmailSettings>(Configuration.GetSection(nameof(SmtpEmailSettings)));

            // Cache setup
            services.AddSingleton<IGenericCache<string>, GenericCache<string>>();

            // Setting up session manager
            services.AddScoped<ISessionManager, SessionManager>();

            // Setting up Auth Service
            services.AddScoped<IAuthService, AuthService>();

            // Configures the event bus.
            ConfigureEventBus(services);
        }

        /// <summary>
        /// Configures the swagger.
        /// </summary>
        /// <param name="services">The services.</param>
        private void ConfigureSwaggerGen(IServiceCollection services)
        {
            _ = services.AddSwaggerGen(c =>
            {
                // configure SwaggerDoc and others
                c.SwaggerDoc("v1", new OpenApiInfo { Title = AssemblyName, Version = "v1" });

                // add JWT Authentication
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, Array.Empty<string>()}
                });
            });
        }

        /// <summary>
        /// Configures the authentication.
        /// </summary>
        /// <param name="services">The services.</param>
        private void ConfigureAuthentication(IServiceCollection services)
        {
            // Load password policy from configurations.
            var dto = ConfigurationHelper.Read(
                                        services.BuildServiceProvider().GetRequiredService<AgentPortalContext>(),
                                        services.BuildServiceProvider().GetRequiredService<IMapper>());

            // Adding MS Identity and Role to the services
            services.AddIdentity<AspNetUser, CustomIdentityRole>(options =>
            {
                // Password policy.
                options.Password = dto.ToPasswordOptions();

                // User settings.
                options.User.RequireUniqueEmail = true;

                // Signin options.
                options.SignIn.RequireConfirmedAccount = true;

                // Token provider.
                options.Tokens.PasswordResetTokenProvider = "Default";
            })
                .AddEntityFrameworkStores<AgentPortalContext>()
                .AddErrorDescriber<CustomIdentityErrorDescriber>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<DefaultDataProtectorTokenProvider<AspNetUser>>("Default")
                .AddPasswordValidator<AgentPortalPasswordValidator>()
                .AddSignInManager<AgentPortalSignInManager>();

            // Read time span for token expiration time from configuration file
            int tokenLifespanInMinutes = Configuration.GetValue<int>(nameof(tokenLifespanInMinutes));

            // Set time span for token expiration
            services.Configure<DefaultDataProtectorTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(tokenLifespanInMinutes);
            });

            // Configure token authentication
            var key = Encoding.UTF8.GetBytes(Configuration[IAuthConstants.JwtSecretKey]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = Configuration[IAuthConstants.JwtIssuer],
                    ValidateAudience = true,
                    ValidAudience = Configuration[IAuthConstants.JwtAudience],
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        /// <summary>
        /// Gets the Sql Connection String
        /// </summary>
        /// <param name="sectionName">The section name.</param>
        /// <returns>A string.</returns>
        private string GetSqlConnectionString(string sectionName)
        {
            SqlConnectionConfigurationDTO config = Configuration.GetSection(sectionName)
                                                                .Get<SqlConnectionConfigurationDTO>();
            return SqlConnectionHelper.ToConnectionString(config);
        }

        /// <summary>
        /// Configures the audit database context.
        /// </summary>
        /// <param name="services">The services.</param>
        private void ConfigureAuditDbContext(IServiceCollection services)
        {
            // Adding Audit Db Context to services
            services.AddDbContext<AgentPortalAuditContext>(options =>
            {
                options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
                options.UseSqlServer(GetSqlConnectionString("ConnectionStrings:AuditConnection"),
                    b => b.MigrationsAssembly(AssemblyName));
            });
        }

        /// <summary>
        /// Configures the event bus.
        /// </summary>
        /// <param name="services">The services collection.</param>
        private void ConfigureEventBus(IServiceCollection services)
        {
            services.AddOptions();

            services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitMq"));
            services.AddTransient<IEventSender, EventSender>();

            services.AddTransient<IQueueHandlerMappingFactory, QueueHandlerMappingFactory>();
            services.AddHostedService<EventListener>();
        }

        /// <summary>
        /// Configures the auto mapper.
        /// </summary>
        /// <param name="services">The services collection.</param>
        private static void ConfigureAutoMapper(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        /// <summary>
        /// Configures the database context.
        /// </summary>
        /// <param name="services">The services.</param>
        private void ConfigureDatabaseContext(IServiceCollection services)
        {
            // Adding Db Context to services
            services.AddDbContext<AgentPortalContext>(options =>
            {
                options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
                options.UseSqlServer(GetSqlConnectionString("ConnectionStrings:SqlConnection"),
                    b => b.MigrationsAssembly(AssemblyName));
            });
        }

        /// <summary>
        /// Configures the.
        /// </summary>
        /// <param name="app">The app.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(a => a.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature.Error;

                    var result = JsonConvert.SerializeObject(new { error = exception.Message });
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(result);
                }));
            }

            // Use Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", AssemblyName));

            // Routing
            app.UseRouting();

            // Authentication
            app.UseAuthentication();

            // Authorization
            app.UseAuthorization();

            // Endpoints
            app.UseEndpoints(endpoints => endpoints.MapControllers().RequireAuthorization());
        }
    }
}
