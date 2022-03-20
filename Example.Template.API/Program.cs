using System.Text;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using Template.Application.Auth;
using Template.IoC;

var builder = WebApplication.CreateBuilder(args);

/////////////////////////// Agregamos los servicios ///////////////////////////

DependencyContainer.RegisterServices(builder);

///////////////////////////////////////////////////////////////////////////////

// Add services to the container.

builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ASP.NET 5 Web API",
        Description = "Authentication"
    });
    // To Enable authorization using Swagger (JWT)
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Description = "Example: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
                      new string[] {}
                    }
                });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Localhost"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<JWTMiddleware>();

app.UseExceptionHandler(x =>
   x.Run(async context =>
   {
       var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
       var exception = errorFeature?.Error;

       if (exception == null) return;

       if (!(exception is ValidationException validationException))
       {
            throw exception;
       }

       var errors = validationException.Errors.Select(err => new
       {
           err.PropertyName,
           err.ErrorMessage
       });
       var errorText = JsonSerializer.Serialize(errors);
       context.Response.StatusCode = 400;
       context.Response.ContentType = "application/json";
       await context.Response.WriteAsync(errorText, Encoding.UTF8);
   })
);

app.MapControllers();

app.Run();
