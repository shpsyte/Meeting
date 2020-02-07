using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WebApp.Hubs {
    public class newLinkHub : Hub {
        public async Task UpdateLink (string link) {
            await Clients.All.SendAsync ("UpdateLink", link);
        }
    }
}