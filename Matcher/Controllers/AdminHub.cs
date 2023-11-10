
using Matcher.Models;
using Microsoft.AspNetCore.SignalR;

namespace Matcher.Controllers
{

    public class AdminHub : Hub
    {
        public async Task LogoutUser(string userId)
        {
            // Gửi thông báo đến máy khách của người dùng
            await Clients.User(userId).SendAsync("UserLoggedOut");

        }
    }

}
