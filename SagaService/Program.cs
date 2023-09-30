using MassTransit;
using Microsoft.EntityFrameworkCore;
using SagaService;
using SagaStateMachine;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContextPool<AppDbContext>(db => db.UseSqlServer(connectionString));
builder.Services.AddMassTransit(cfg =>
{
    cfg.AddBus(provider=> MessageBrokers.RabbitMQProvider.ConfigureBus(provider));
    cfg.AddSagaStateMachine<TicketStateMachine, TicketStateData>()
        .EntityFrameworkRepository(r =>
        {
            r.ConcurrencyMode = ConcurrencyMode.Pessimistic; // or use Optimistic, which requires RowVersion

            r.ExistingDbContext<AppDbContext>();
        });
});
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();