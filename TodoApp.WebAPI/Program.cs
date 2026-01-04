using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Services;
using TodoApp.Infrastructure.Data;
using TodoApp.Infrastructure.Repositories;
using TodoApp.Infrastructure.Services;
using TodoApp.WebAPI.Filters;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TodoApp.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
           // Configure Npgsql to use timestamp without time zone
           AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
           
           var builder = WebApplication.CreateBuilder(args);

// Controllers + Global Filter
builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});

// ✅ MediatR - CQRS Pattern
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(TodoApp.Application.Features.Users.Commands.CreateUser.CreateUserCommand).Assembly);
});

// ✅ FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(TodoApp.Application.Features.Users.Commands.CreateUser.CreateUserCommand).Assembly);

// ✅ CORS - Đặt tên policy rõ ràng
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ JWT Service
builder.Services.AddScoped<IJwtService, JwtService>();

// ✅ JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!))
    };
});

builder.Services.AddAuthorization();

// DI services - Giữ lại cho ToDoList/ToDoItem (chưa migrate sang CQRS)
builder.Services.AddScoped<IToDoItemService, ToDoItemService>();
builder.Services.AddScoped<IToDoListService, ToDoListService>();

// DI repositories
builder.Services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IToDoListRepository, ToDoListRepository>();

// DB
var useInMemoryDB = builder.Configuration.GetValue<bool>("UseInMemoryDB");

if (useInMemoryDB)
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("TodoAppInMemoryDb"));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
}


var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

// ✅ CORS PHẢI Ở TRƯỚC UseAuthorization
app.UseCors("AllowVueApp");

// ✅ Authentication PHẢI trước Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

        }

    }
}