using Microsoft.AspNet.SignalR;

namespace MartManagement.WebApp
{
    public class NotificationHub : Hub
    {
        public static void Send()
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.displayStatus("Test message from server");
        }
    }
}