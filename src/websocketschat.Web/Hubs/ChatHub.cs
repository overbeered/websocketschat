using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using websocketschat.Core.Models;
using websocketschat.Core.Services.Interfaces;
using websocketschat.Web.Helpers.MessageHandler;

namespace websocketschat.Web.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;
        private readonly MessageHandler _messageHandler;
        private readonly Dictionary<Guid,User> _users;
        private readonly IUserService _userService;
        public ChatHub(ILogger<ChatHub> logger, MessageHandler messageHandler, IUserService userService)
        {
            _logger = logger;
            _messageHandler = messageHandler;
            _users = new Dictionary<Guid,User>();
            _userService = userService;
        }
        public async Task Send(string message, string userName)
        {
            Tuple<bool,string> handledMessage = await _messageHandler.HandleAsync(userName, message);

            Guid userId = Guid.Parse(Context.User.FindFirstValue("Guid"));

            var connectedUser = await _userService.GetUserByIdAsync(userId);

            // true  -  if message was command.
            // false  -  if message was simple text. 
            if (handledMessage.Item1)
            {
                await Clients.All.SendAsync("Notify", "Bot: " + handledMessage.Item2);
            }
            else
            {
                await Clients.All.SendAsync("Receive", message, connectedUser.Username);
            }
        }
        public override async Task OnConnectedAsync()
        {
            User connectedUser = await _userService.GetUserAsync(Context.GetHttpContext().User.Identity.Name);
            _users.Add(connectedUser.Id, connectedUser);

            Console.WriteLine("Added: " + connectedUser);

            await Clients.All.SendAsync("Notify", $"{connectedUser.Username} вошел в чат");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Guid userId = Guid.Parse(Context.User.FindFirstValue("Guid"));

            var connectedUser = await _userService.GetUserByIdAsync(userId);
            //User connectedUser = _users[userId];
            //_users.Remove(userId);

            Console.WriteLine("Removed: " + connectedUser);

            await Clients.All.SendAsync("Notify", $"{connectedUser.Username} покинул в чат");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
