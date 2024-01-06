using Microsoft.AspNetCore.SignalR;

namespace AbsManagementAPI.Core.HubSignalR
{
    public interface INotifyService
    {
        Task SendMessageNotify(string type, string message);
        Task SendMessageNotifyOnPhuongQuan(string type, string message, string quan, string phuong);
    }

    public class NotifyService : INotifyService
    {
        private readonly IHubContext<NotifyHub> _hubContext;

        public NotifyService(IHubContext<NotifyHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessageNotify(string type, string message)
        {
            await _hubContext.Clients.All.SendAsync("onNotify", type, message);
        }


        public async Task SendMessageNotifyOnPhuongQuan(string type, string message,string quan,string phuong)
        {
            await _hubContext.Clients.All.SendAsync("onNotify", type, message,phuong,quan);
        }
    }
}
