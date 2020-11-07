using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using websocketschat.Core.Services.Interfaces;
using websocketschat.Web.Helpers.MessageHandler;

namespace websocketschat.Web.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;
        private readonly MessageHandler _messageHandler;
        public ChatHub(ILogger<ChatHub> logger, MessageHandler messageHandler)
        {
            _logger = logger;
            _messageHandler = messageHandler;
        }
        public async Task Send(string message, string userName)
        {
            string senderUserRole = Context.User.FindFirst(ClaimTypes.Role)?.Value;

            string finalMessage = await _messageHandler.HandleAsync(userName, message);

            await Clients.All.SendAsync("Receive", finalMessage, userName);
        }
        public override async Task OnConnectedAsync()
        {
#if DEBUG
            Console.WriteLine($"{Context.GetHttpContext().User.Identity.Name} вошел в чат");
#endif
            await Clients.All.SendAsync("Notify", $"{Context.GetHttpContext().User.Identity.Name} вошел в чат");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
#if DEBUG
            Console.WriteLine("Отключился " + Context.GetHttpContext().User.Identity.Name);
#endif
            await Clients.All.SendAsync("Notify", $"{Context.GetHttpContext().User.Identity.Name} покинул в чат");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
