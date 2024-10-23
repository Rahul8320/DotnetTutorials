using Microsoft.AspNetCore.SignalR;

namespace Notification.Api.Notifications;

public class NotificationsHub : Hub<INotificationClient>
{
    public override async Task OnConnectedAsync()
    {
        await Clients.Client(Context.ConnectionId).ReceiveNotification(
            $"Thank you for connecting {Context.User?.Identity?.Name}"
        );

        await base.OnConnectedAsync();
    }
}


public interface INotificationClient
{
    Task ReceiveNotification(string message);
}