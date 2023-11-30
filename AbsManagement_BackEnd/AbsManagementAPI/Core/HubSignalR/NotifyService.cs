using Microsoft.AspNetCore.SignalR;

namespace AbsManagementAPI.Core.HubSignalR
{
    public interface INotifyService
    {
        Task SendMessageNotify(string title, string message);
    }

    public class NotifyService : INotifyService
    {
        private readonly IHubContext<NotifyHub> _hubContext;

        public NotifyService(IHubContext<NotifyHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessageNotify(string title, string message)
        {
            await _hubContext.Clients.All.SendAsync("onNotify", title, message);
        }
    }
}
