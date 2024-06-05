using Common.Helper;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Repository;
using OrderService.Inbound.Port;
using OrderService.Outbound.Adapter;
using OrderService.Outbound.Port;
using OrderService.Inbound.Adapter;

var builder = WebApplication.CreateBuilder(args);

string connectionString = $"Host={builder.Configuration["PostgreSQL:Host"]};Database={builder.Configuration["PostgreSQL:Database"]};Username={builder.Configuration["PostgreSQL:Username"]};Password={builder.Configuration["PostgreSQL:Password"]}";
builder.Services.AddDbContextFactory<OrderRepository>(options => options.UseNpgsql(connectionString));

builder.Services.AddSingleton<OrderRepository>();
builder.Services.AddSingleton<IInboundRestPort, OrderService.Service.OrderService>();
builder.Services.AddSingleton<IInboundMessagingPort, OrderService.Service.OrderService>();
builder.Services.AddSingleton<IOutboundPersistencePort, OutboundPersistenceAdapter>();
builder.Services.AddSingleton<IOutboundMessagingPort, MessagingOutboundAdapter>();

builder.Services.AddSingleton<MessagePublisher>();


builder.Services.AddControllers();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Services.AddHostedService<MessagingInboundAdapter>();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();
