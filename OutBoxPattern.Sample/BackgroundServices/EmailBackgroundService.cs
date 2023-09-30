using OutBoxPattern.Sample.Services;

namespace OutBoxPattern.Sample.BackgroundServices;

public class EmailBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<EmailBackgroundService> _logger;
    
    public EmailBackgroundService(
        IServiceScopeFactory serviceScopeFactory,
        ILogger<EmailBackgroundService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timer = new System.Timers.Timer();
        timer.Interval = TimeSpan.FromMinutes(1).TotalMilliseconds;
        timer.Elapsed += Timer_Elapsed;
        timer.Start();
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
    private async void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        _logger.LogInformation("Messages are sending.");
        Send();
        await Task.Yield();
    }
    
    private void Send()
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var emailService = scope.ServiceProvider.GetRequiredService<IMailService>();
            var emailOutboxService = scope.ServiceProvider.GetRequiredService<IEmailOutbox>();
            var allOutboxResult = emailOutboxService.GetAll();
            if (allOutboxResult.Any())
            {
                foreach (var item in allOutboxResult)
                {
                    var res = emailService.Send(item.Order.Email, "Order is completed", "Your order has been saved in the database", false);
                    if(res)
                    {
                        var updateResult = emailOutboxService.Update(item).Result;
                    }
                }
            }
        }
    }
}