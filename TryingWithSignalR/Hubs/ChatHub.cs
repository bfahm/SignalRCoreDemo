using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace SignalRChatServer
{
    public class ChatHub : Hub
    {

        #region Consts, Fields, Properties, Events

        #endregion

        #region Methods

        public void Send(string name, string message)
        {
            // Call the "OnMessage" method to update clients.
            Clients.All.SendCoreAsync("OnMessage", new object[] { name, message });
        }

        public override async Task OnConnectedAsync()
        {
            //await Clients.All.SendAsync("UserConnected", Context.ConnectionId);
            await base.OnConnectedAsync();
            Console.WriteLine("Some unknown connected to us.");
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            //await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId);
            await base.OnDisconnectedAsync(ex);
            Console.WriteLine("Some unknown lost connection.");
        }

        #endregion
    }
}