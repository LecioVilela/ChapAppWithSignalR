using Microsoft.AspNetCore.SignalR;

namespace ChatAppWithSignalR.API.Hubs
{
    public class ChatHub : Hub
    {
        /// <summary>
        /// Here we're gonna send the message to all clients!
        /// </summary>
        /// <param name="group"></param>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(string group, string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        /// <summary>
        /// This method will use for put a user in a group
        /// </summary>
        /// <param name="user"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public async Task JoinGroup(string user, string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);

            await Clients.Group(group)
                         .SendAsync("ReceiveMessage",
                                    "Server",
                                    $"{user} Connection: {Context.ConnectionId} has joined in the group! {group}"
                                    );

        }
    }
}
