using MassTransit;
using Microsoft.EntityFrameworkCore;
using TicketService;
using TicketService.Consumers;
using TicketService.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DbConnection");

builder.Services.AddScoped<ITicketServices, TicketServices>();
builder.Services.AddDbContextPool<AppDbContext>(db => db.UseSqlServer(connectionString));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMassTransit(cfg =>
{
    cfg.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        cfg.ReceiveEndpoint(MessageBrokers.RabbitMQQueues.SagaBusQueue, ep =>
        {
            ep.PrefetchCount = 10;
            // Get Consumer
            ep.ConfigureConsumer<GetValueConsumer>(provider);
        });
    }));

    cfg.AddConsumer<GetValueConsumer>();
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