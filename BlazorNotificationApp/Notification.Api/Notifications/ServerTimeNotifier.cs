using Microsoft.AspNetCore.SignalR;

namespace Notification.Api.Notifications;

public class ServerTimeNotifier(
    IHubContext<NotificationsHub, INotificationClient> hubContext,
    ILogger<ServerTimeNotifier> logger) : BackgroundService
{
    private static readonly TimeSpan Period = TimeSpan.FromSeconds(5);
    private readonly ILogger<ServerTimeNotifier> _logger = logger;
    private readonly IHubContext<NotificationsHub, INotificationClient> _hubContext = hubContext;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(Period);

        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            var dateTime = DateTime.Now;

            _logger.LogInformation("Executing {Service} {Time}", nameof(ServerTimeNotifier), dateTime);

            await _hubContext.Clients.All.ReceiveNotification($"Server time: {dateTime}");
        }
    }
}
