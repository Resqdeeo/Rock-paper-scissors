using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RockPaperScissors.Application.Abstractions;
using RockPaperScissors.Application.DTOs.Auth;
using RockPaperScissors.Application.DTOs.Auth.Register;
using RockPaperScissors.Application.Features.Auth.Login;
using RockPaperScissors.Application.Features.Auth.Register;
using RockPaperScissors.Application.Services;
using RockPaperScissors.Domain.Repositories;
using RockPaperScissors.Infrastructure.CQRS;
using RockPaperScissors.Infrastructure.Services;
using RockPaperScissors.Persistence;
using RockPaperScissors.Persistence.Repositories;
using RockPaperScissors.WebAPI.Configurations;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();
builder.Services.AddScoped<ICommandHandler<LoginCommand, LoginResponse>, LoginCommandHandler>();
builder.Services.AddScoped<ICommandHandler<RegisterCommand, RegisterResponse>, RegisterCommandHandler>();
builder.Services.AddScoped<ICommandHandler<LoginCommand, LoginResponse>, LoginCommandHandler>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
        };
    });



builder.Services.AddControllers();

builder.Services.AddDbContext<GameDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.UseHttpsRedirection();



app.Run();
