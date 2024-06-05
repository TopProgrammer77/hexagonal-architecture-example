using BillingService.Inbound.Port;
using BillingService.Outbound.Adapter;
using BillingService.Outbound.Port;
using BillingService.Service;
using BillingService.Inbound.Adapter;
using Common.Marker.Adapter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IOutboundRestPort, PaymentService>();
builder.Services.AddSingleton<IOutboundMessagingPort, MessageProducer>();
builder.Services.AddSingleton<IInboundRestPort, BillingService.Service.BillingService>();
builder.Services.AddSingleton<IInboundMessagingPort, BillingService.Service.BillingService>();

builder.Services.AddSingleton<Common.Helper.MessagePublisher>();


builder.Services.AddControllers();
builder.Services.AddHostedService<MessageConsumer>();

builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();
app.UseRouting();
app.MapControllers();

app.Run();
