using System.Reflection;
using System.Text;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Template.Application.Handlers;
using Template.Application.Logic;
using Template.Application.Mediator.PipelineBehaviors;
using Template.Application.Models;
using Template.Domain.AppSettings;
using Template.Domain.Core;
using Template.Infrastructure.Context;
using Template.Infrastructure.Generics;

namespace Template.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection RegisterServices(this WebApplicationBuilder builder)
        {
            // Configuration
            ConfigurationSettings configurationSettings = new ConfigurationSettings();
            builder.Configuration.GetSection(ConfigurationSettings.Configuration).Bind(configurationSettings);
            IOptions<ConfigurationSettings> config = Options.Create(configurationSettings);
            builder.Services.AddSingleton(config);

            //////////////////////

            // For Identity
            //builder.Services.AddIdentity<AppUser, IdentityRole>()
            //.AddEntityFrameworkStores<ApplicationDbContext>()
            //.AddDefaultTokenProviders();
            // Adding Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = config.Value.JWT?.ValidIssuer,
                    ValidAudience = config.Value.JWT?.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Value.JWT?.SecretKey ?? string.Empty))
                };
            });

            // CurrentUser
            builder.Services.AddHttpContextAccessor();

            // MediatR
            builder.Services.AddMediatR(typeof(BaseCommand).GetTypeInfo().Assembly);
            
            // AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile()); // Clase donde agregamos la definición de los mapeos.
            });
            IMapper mapper = mappingConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            // Fluent Validations
            builder.Services.AddValidatorsFromAssembly(typeof(ValidationPipelineBehavior<,>).Assembly);

            // Pipeline Behaviours
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

            // Logic
            builder.Services.AddScoped<IUnitLogic, UnitLogic>();

            // Infrastructure
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(config.Value.ConnectionStrings?.SqlDB ?? string.Empty));


            return builder.Services;
        }
    }
}
