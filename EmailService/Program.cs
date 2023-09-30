using EmailService.Consumers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMassTransit(cfg =>
{
    cfg.AddBus(provider => MessageBrokers.RabbitMQProvider.ConfigureBus(provider));
    cfg.AddConsumer<SendEmailConsumer>();
});
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();