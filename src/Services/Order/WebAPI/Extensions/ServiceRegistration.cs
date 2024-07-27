using Domain.Repositories;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, ConnectionStrings connection)
        {
            services.AddDbContext<OrderAppDbContext>(options =>
            {
                options.UseSqlServer(connection.MSSQL);
            });

            services.AddScoped<IOrderRepository, OrderRepository>();
        }
    }
}
