﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TryingWithSignalR.Hubs
{
    public class MessageHub : Hub
    {
        public Task SendMessageToAll(string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", message);
        }

        public Task SendMessageToCaller(string message)
        {
            return Clients.Caller.SendAsync("ReceiveMessage", message);
        }

        public Task SendMessageToUser(string connectionId, string message)
        {
            return Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
        }

        public Task JoinGroup(string group)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public Task SendMessageToGroup(string group, string message)
        {
            return Clients.Group(group).SendAsync("ReceiveMessage", message);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("UserConnected", Context.ConnectionId);
            await base.OnConnectedAsync();
            Console.WriteLine("Some unknown connected to us.");
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId);
            await base.OnDisconnectedAsync(ex);
            Console.WriteLine("Some unknown lost connection.");
        }
    }
}
