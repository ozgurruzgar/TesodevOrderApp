using Application.Consumers;
using Application.Extensions;
using Domain.Services;
using MassTransit;
using TesodevOrderApp.Shared.Domain.Constants;
using WebAPI.Extensions;
using WebAPI.Mapping;
using WebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionStrings = builder.Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(connectionStrings);

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddAutoMapper(typeof(OrderProfile).Assembly);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SendAddressMessageConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {

        cfg.Host(connectionStrings.RabbitMQ, "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        cfg.ReceiveEndpoint(Constants.Queues.SendAddressQueue, e =>
        {
            e.ConfigureConsumer<SendAddressMessageConsumer>(context);
        });
    });
});

builder.Services.AddOptions<MassTransitHostOptions>()
    .Configure(options =>
    {
        options.WaitUntilStarted = true;
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
