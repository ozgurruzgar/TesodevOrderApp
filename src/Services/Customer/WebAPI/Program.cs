using Domain.Services;
using Application.Extensions;
using WebAPI.Extensions;
using WebAPI.Mapping;
using WebAPI.Models;
using MassTransit;
using Application.Consumers;
using TesodevOrderApp.Shared.Domain.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionStrings = builder.Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(connectionStrings);

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddAutoMapper(typeof(CustomerProfile).Assembly);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<FillAddressMessageConsumer>();
    x.AddConsumer<UpdateCustomerAddressMessageConsumer>();

    // Default Port : 5672
    x.UsingRabbitMq((context, cfg) =>
    {

        cfg.Host(connectionStrings.RabbitMQ, "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        cfg.ReceiveEndpoint(Constants.Queues.FillAddressQueue, e =>
        {
            e.ConfigureConsumer<FillAddressMessageConsumer>(context);
        });

        cfg.ReceiveEndpoint(Constants.Queues.UpdateCustomerAddressQueue, e =>
        {
            e.ConfigureConsumer<UpdateCustomerAddressMessageConsumer>(context);
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
