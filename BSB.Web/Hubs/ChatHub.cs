using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BSB.Data;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace BSB.Web.Hubs
{
    public class ChatHub : Hub 
    {
        public static List<ConnectionMapping> connectionMappings = new List<ConnectionMapping>();

        public async Task SendMessage(string sender, string message)
        {
            if (message.Trim().Equals(""))
                return;

            var senderConnId = connectionMappings.Where(x => x.UserId.Equals( sender)).FirstOrDefault().ConnectionId;

            await Clients.All.SendAsync("ReceiveMessage", message, sender);
        }

        public override async Task OnConnectedAsync()
        {
            connectionMappings[connectionMappings.Count - 1].ConnectionId = Context.ConnectionId;
            await Clients.All.SendAsync("UserConnected", connectionMappings[connectionMappings.Count - 1].UserId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            var res = connectionMappings[connectionMappings.Count - 1];
            connectionMappings.RemoveAt(connectionMappings.Count - 1);
            await Clients.All.SendAsync("UserDisconnected", res.UserId);
            await base.OnDisconnectedAsync(ex);
        }

    }
}