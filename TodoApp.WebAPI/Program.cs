using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Services;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Enums;
using TodoApp.Infrastructure.Data;
using TodoApp.Infrastructure.Repositories;
using TodoApp.WebAPI.Filters;
namespace TodoApp.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            var useInMemoryDB = builder.Configuration.GetValue<bool>("UseInMemoryDB");

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            });

            // Adds Microsoft Identity platform (AAD v2.0) support to protect this Api
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddMicrosoftIdentityWebApi(options =>

                    {
                        configuration.Bind("AzureAd", options);
                        options.Events = new JwtBearerEvents();

                        options.Events = new JwtBearerEvents
                        {
                            OnTokenValidated = context =>
                            {
                                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();

                                // Access the scope claim (scp) directly
                                var scopeClaim = context.Principal?.Claims.FirstOrDefault(c => c.Type == "scp")?.Value;

                                if (scopeClaim != null)
                                {
                                    logger.LogInformation("Scope found in token: {Scope}", scopeClaim);
                                }
                                else
                                {
                                    logger.LogWarning("Scope claim not found in token.");
                                }

                                return Task.CompletedTask;
                            },
                            OnAuthenticationFailed = context =>
                            {
                                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                                logger.LogError("Authentication failed: {Message}", context.Exception.Message);
                                return Task.CompletedTask;
                            },
                            OnChallenge = context =>
                            {
                                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                                logger.LogError("Challenge error: {ErrorDescription}", context.ErrorDescription);
                                return Task.CompletedTask;
                            }
                        };
                    }, options => { configuration.Bind("AzureAd", options); });

            // The following flag can be used to get more descriptive errors in development environments
            IdentityModelEventSource.ShowPII = false;

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger TODO App Demo", Version = "v1" });
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "OAuth2.0 Auth Code with PKCE",
                    Name = "oauth2",
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"https://login.microsoftonline.com/{configuration["AzureAd:TenantId"]}/oauth2/v2.0/authorize"),
                            TokenUrl = new Uri($"https://login.microsoftonline.com/{configuration["AzureAd:TenantId"]}/oauth2/v2.0/token"),//token end point
                            Scopes = new Dictionary<string, string>
                                {
                                    { $"api://{configuration["AzureAd:ClientId"]}/App.Read", "Read access to ToDo App API" },
                                    { $"api://{configuration["AzureAd:ClientId"]}/App.Write", "Write access to ToDo App API" }
                                }
                        }
                    }
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                        },
                        new[] { $"api://{configuration["AzureAd:ClientId"]}/App.Read", 
                            $"api://{configuration["AzureAd:ClientId"]}/App.Write" } //Scope details
        }
                });
            });

            builder.Services.AddScoped<IToDoItemService, ToDoItemService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IToDoListService, ToDoListService>();

            builder.Services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IToDoListRepository, ToDoListRepository>();


            if (useInMemoryDB)
            {

                // we could have written that logic here but as per clean architecture, we are separating these into their own piece of code
                builder.Services.AddInMemoryDatabase();
            }
            else
            {
                //use this for real database on your sql server
                builder.Services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DbContext"),
                    providerOptions => providerOptions.EnableRetryOnFailure()
                    );
                }
                  );
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo Tracker API v1");
                    c.OAuthClientId(configuration["AzureAd:SwaggerClientId"]);
                    c.OAuthUsePkce(); // Enables PKCE flow for security
                    c.OAuthScopeSeparator(" ");
                }
                    );
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();



            app.MapControllers();

            if (useInMemoryDB)
            {
                // Seed data. Use this config for in memory database
                using (var scope = app.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    SeedData(context); // Call to seed data
                }
            }
            else
            {
                //User this if you want database on your sql server
                // Automatically create the database if it does not exist. This is required only for real database
                using (var scope = app.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    context.Database.EnsureCreated(); // This creates the database if it doesn't exist
                                                      //Seeding initial data is taken care from OnModelCreating of AppDbContext class.
                }
            }

            app.Run();
        }

        private static void SeedData(AppDbContext context)
        {
            context.Users.AddRange(
                new User { UserId = 1, Username = "JohnDoe", Email = "john@example.com", CreatedAt = DateTime.Now },
                new User { UserId = 2, Username = "JaneDoe", Email = "jane@example.com", CreatedAt = DateTime.Now }
            );

            context.ToDoLists.AddRange(
                new ToDoList { ToDoListId = 1, UserId = 1, Title = "Groceries", Description = "Things to buy", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new ToDoList { ToDoListId = 2, UserId = 1, Title = "Work", Description = "Work-related tasks", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
            );

            context.ToDoItems.AddRange(
                new ToDoItem { ToDoItemId = 1, ToDoListId = 1, Title = "Buy milk", Description = "Get whole milk", DueDate = DateTime.Now.AddDays(2), IsCompleted = false, Priority = PriorityLevel.Medium, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new ToDoItem { ToDoItemId = 2, ToDoListId = 1, Title = "Buy eggs", Description = "Get a dozen eggs", DueDate = DateTime.Now.AddDays(3), IsCompleted = false, Priority = PriorityLevel.High, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
            );

            context.SaveChanges(); // Save changes to the in-memory database
        }
    }
}
