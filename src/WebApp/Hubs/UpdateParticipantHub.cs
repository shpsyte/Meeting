using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WebApp.ViewModels;

namespace WebApp.Hubs {
    public class newParticipantHub : Hub {
        public async Task SendMessage (MeetingViewModel data) {
            await Clients.All.SendAsync ("SendMessage", data);
        }
    }
}