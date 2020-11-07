﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websocketschat.Web.Helpers.MessageHandler;

namespace websocketschat.Web.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;
        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger;
        }
        public async Task Send(string message, string userName)
        {
            string finalMessage = CommandHandler.Execute(message);

            await Clients.All.SendAsync("Receive", finalMessage, userName);
        }
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("Подключился " + Context.GetHttpContext().User.Identity.Name);
            await Clients.All.SendAsync("Notify", $"{Context.GetHttpContext().User.Identity.Name} вошел в чат");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("Отключился " + Context.GetHttpContext().User.Identity.Name);
            await Clients.All.SendAsync("Notify", $"{Context.GetHttpContext().User.Identity.Name} покинул в чат");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
