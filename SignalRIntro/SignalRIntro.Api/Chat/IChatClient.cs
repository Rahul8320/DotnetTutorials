namespace SignalRIntro.Api.Chat;

public interface IChatClient
{
    Task ReceiveMessage(string message);
}
