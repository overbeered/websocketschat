using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using websocketschat.Core.Models;
using websocketschat.Core.Services.Interfaces;

namespace websocketschat.Web.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;
        private readonly IUserService _userService;
        public ChatHub(ILogger<ChatHub> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        public async Task Send(string username, string text)
        {
            Console.WriteLine("Username: " + username);

            Guid userId = Guid.Parse(Context.User.FindFirstValue("Guid"));
            User connectedUser = await _userService.GetUserByIdAsync(userId);

            string responseMessage = text;

            if (responseMessage == string.Empty || responseMessage == null)
            {
                await Clients.User(connectedUser.Id.ToString()).SendAsync("Receive", new
                {
                    message = "--> Message can't be null or empty.",
                    sender_username = "Bot",
                    getter_username = connectedUser.Username,
                    roleid = 3
                });
                return;
            }

            // starts with '/'  -  if text is command.
            // else -  if text is message.
            if (text.StartsWith("/"))
            {
                text = text.Remove(0, 1);

                // Chat commands.
                // /commands
                if (text.ToLower().Contains("commands"))
                {
                    responseMessage = "/change_name=newName - change nickname if new nickname is free to pick.\n" +
                                      "/send_to=username&message=text - sends private message (text param) to user (username param) if exists.\n" +
                                      "/commands - shows stored commands.";

                    await Clients.User(connectedUser.Id.ToString()).SendAsync("Receive", new
                    {
                        message = "-->" + responseMessage,
                        sender_username = "Bot",
                        getter_username = connectedUser.Username,
                        roleid = 3
                    });
                    return;
                }

                // Change current user nickname.
                // /change_name=nickname
                else if (text.ToLower().Contains("change_name="))
                {
                    string newNickname = text.Substring(12);

                    if (newNickname == string.Empty || newNickname == null)
                    {
                        responseMessage = $"You tried to change your username to \'{newNickname}\' with failure.";

                        await Clients.User(connectedUser.Id.ToString()).SendAsync("Receive", new
                        {
                            message = "-->" + responseMessage,
                            sender_username = "Bot",
                            getter_username = connectedUser.Username,
                            roleid = 3
                        });
                        return;
                    }
                    else
                    {
                        User userWithSameUsername = await _userService.GetUserAsync(newNickname);

                        if (userWithSameUsername == null)
                        {
                            User user = await _userService.GetUserAsync(username);

                            user.Username = newNickname;

                            User updatedUser = await _userService.UpdateUserAsync(user);

                            responseMessage = $"User \'{username}\' changed nickname to \'{updatedUser.Username}\'.";

                            await Clients.All.SendAsync("Receive", new
                            {
                                message = responseMessage,
                                sender_username = updatedUser.Username,
                                getter_username = "",
                                roleid = updatedUser.RoleId
                            });
                            return;
                        }
                        else
                        {
                            responseMessage = $"You tried to change nickname to \'{newNickname}\' but user with this nickname is belong to other user.";
                            await Clients.User(connectedUser.Id.ToString()).SendAsync("Receive", new
                            {
                                message = "-->" + responseMessage,
                                sender_username = "Bot",
                                getter_username = connectedUser.Username,
                                roleid = 3
                            });
                            return;
                        }
                    }
                }

                // Private message.
                // /send_to=username&message=text
                else if (text.ToLower().StartsWith("send_to="))
                {
                    string to = text.Split(new char[] { '&' })[0].Substring(8);
                    string message = text.Split(new char[] { '&' })[1].Substring(8);

                    User userMessageGetter = await _userService.GetUserAsync(to);

                    if (userMessageGetter == null)
                    {
                        responseMessage = $"User with username \'{to}\' does not exist.";
                        await Clients.User(connectedUser.Id.ToString()).SendAsync("Receive", new
                        {
                            message = "-->" + responseMessage,
                            sender_username = "Bot",
                            getter_username = connectedUser.Username,
                            roleid = 3
                        });
                        return;
                    }

                    // if sender is a getter too.
                    if (userMessageGetter != connectedUser)
                    {

                        if (message == string.Empty || message == null)
                        {
                            await Clients.User(connectedUser.Id.ToString()).SendAsync("Receive", new
                            {
                                message = "--> Message can't be null or empty.",
                                sender_username = "Bot",
                                getter_username = connectedUser.Username,
                                roleid = 3
                            });
                            return;
                        }

                        // write to sender what his message was sent to recevier.
                        responseMessage = $"\'{message}\' was sent to {userMessageGetter.Username} successfully.";
                        await Clients.User(connectedUser.Id.ToString()).SendAsync("Receive", new
                        {
                            message = "-->" + responseMessage,
                            sender_username = "Bot",
                            getter_username = connectedUser.Username,
                            roleid = 3
                        });

                        // print message on receiver screen.
                        await Clients.User(userMessageGetter.Id.ToString()).SendAsync("Receive", new
                        {
                            message = "-->" + message,
                            sender_username = connectedUser.Username,
                            getter_username = userMessageGetter.Username,
                            roleid = connectedUser.RoleId
                        });
                        return;
                    }
                    else
                    {
                        responseMessage = $"You tried to send message to yourself.";
                        await Clients.User(connectedUser.Id.ToString()).SendAsync("Receive", new
                        {
                            message = "-->" + responseMessage,
                            sender_username = "Bot",
                            getter_username = connectedUser.Username,
                            roleid = 3
                        });
                        return;
                    }
                }

                // Not a command
                else
                {
                    responseMessage = "Your typed command " + "\'/" + text + "\' is unsupported.";
                    await Clients.User(connectedUser.Id.ToString()).SendAsync("Receive", new
                    {
                        message = "-->" + responseMessage,
                        sender_username = "Bot",
                        getter_username = connectedUser.Username,
                        roleid = 3
                    });
                    return;
                }
            }
            else
            {
                await Clients.All.SendAsync("Receive", new
                {
                    message = text,
                    sender_username = connectedUser.Username,
                    getter_username = "",
                    roleid = connectedUser.RoleId
                });
            }
        }

        #region Connect/Disconnect stuff
        public override async Task OnConnectedAsync()
        {
            User connectedUser = await _userService.GetUserAsync(Context.GetHttpContext().User.Identity.Name);

            await Clients.All.SendAsync("Notify", $"{connectedUser.Username} joined the chat.");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Guid userId = Guid.Parse(Context.User.FindFirstValue("Guid"));
            User connectedUser = await _userService.GetUserByIdAsync(userId);

            await Clients.All.SendAsync("Notify", $"{connectedUser.Username} left the chat.");
            await base.OnDisconnectedAsync(exception);
        }
        #endregion
    }
}
