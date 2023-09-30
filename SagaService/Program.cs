using MassTransit;
using Microsoft.EntityFrameworkCore;
using SagaService;
using SagaStateMachine;

IHostBuilder builder = Host.CreateDefaultBuilder(args);
if (OperatingSystem.IsWindows())
{
    builder.UseWindowsService();
}

if (OperatingSystem.IsLinux())
{
    builder.UseSystemd();
}

IHost host = builder.ConfigureServices((hostContext, services) =>
    {
        var connectionString = hostContext.Configuration.GetConnectionString("DbConnection");
        services.AddDbContextPool<AppDbContext>(db => db.UseSqlServer(connectionString));
        services.AddMassTransit(cfg =>
        {
            cfg.AddBus(provider=> MessageBrokers.RabbitMQProvider.ConfigureBus(provider));
            cfg.AddSagaStateMachine<TicketStateMachine, TicketStateData>()
                .EntityFrameworkRepository(r =>
                {
                    r.ConcurrencyMode = ConcurrencyMode.Pessimistic; // or use Optimistic, which requires RowVersion

                    r.ExistingDbContext<AppDbContext>();
                });
        });
    }).Build();

host.Run();