
using ePizzaHub.API.Middleware;
using ePizzaHub.Core.Concrete;
using ePizzaHub.Core.Contracts;
using ePizzaHub.Infrastructure.Models;
using ePizzaHub.Repositories.Concrete;
using ePizzaHub.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using Repositories.Concrete;
using Repositories.Contract;

namespace ePizzaHub.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ePizzaHubContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"),
                             sqlOptions => sqlOptions.EnableRetryOnFailure()
                     );
            });
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IItemRepository, ItemRepository>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();



            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddTransient<IItemService, ItemService>();
            builder.Services.AddTransient<ICartService, CartService>();
            builder.Services.AddTransient<ITokenGeneratorService, TokenGeneratorService>();





            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
             
            app.UseAuthorization();

            app.UseMiddleware<CommonReponseMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }
}
