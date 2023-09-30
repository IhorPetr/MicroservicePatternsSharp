using GenerateTicket;
using GenerateTicket.Consumers;
using GenerateTicket.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddScoped<ITicketInfoService, TicketInfoService>();
builder.Services.AddDbContextPool<AppDbContext>(db => db.UseSqlServer(connectionString));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMassTransit(cfg =>
{
    cfg.AddBus(provider => MessageBrokers.RabbitMQProvider.ConfigureBus(provider));
    cfg.AddConsumer<GenerateTicketConsumer>();
    cfg.AddConsumer<CancelSendingEmailConsumer>();
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();