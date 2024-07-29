using Application.Consumers;
using Application.Extensions;
using AutoMapper;
using Domain.Configurations;
using Domain.Services;
using Hangfire;
using MassTransit;
using MediatR;
using TesodevOrderApp.Shared.Domain.Constants;
using WebAPI.Extensions;
using WebAPI.Jobs;
using WebAPI.Mapping;
using WebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionStrings = builder.Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(connectionStrings);

var emailConfiguration = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfiguration);

var serviceApiSettings = builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();
builder.Services.AddSingleton(serviceApiSettings);

builder.Services.AddHttpClient();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IEmailService, EmailService>();
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

builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(connectionStrings.Hangfire);
});
builder.Services.AddHangfireServer();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var serviceProvider = app.Services;

var jobManager = serviceProvider.GetRequiredService<IRecurringJobManager>();
var mediator = serviceProvider.GetRequiredService<IMediator>();
var mapper = serviceProvider.GetRequiredService<IMapper>();
var httpClient = serviceProvider.GetRequiredService<HttpClient>();
var sendMail = new SendEmailJob(mediator, mapper, httpClient);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHangfireDashboard();
RecurringJob.AddOrUpdate("sendMail1", () => sendMail.SendEmail(), Cron.Daily);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHangfireDashboard();

app.Run();
