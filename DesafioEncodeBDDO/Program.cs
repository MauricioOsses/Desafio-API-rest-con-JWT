using DesafioEncodeBDDO.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DesafioEncodeBDDO.Helpers;
using DesafioEncodeBDDO.Services;
using AutoMapper;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckl ne
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSqlEncode")));

//builder.Services.AddControllers().AddJsonOptions(opt => { opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(op =>
{
    op.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization(op =>
    op.AddPolicy("Admin", policy => policy.RequireClaim("Rol", "Administrador"))
);

builder.Services.AddScoped<LoginService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler(builder => 
    {builder.Run(async context => 
    { context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var error = context.Features.Get<IExceptionHandlerFeature>();
        if(error != null)
        {
            context.Response.AddApplicationError(error.Error.Message);
            await context.Response.WriteAsync(error.Error.Message);
        }
    }); 
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
