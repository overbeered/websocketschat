using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websocketschat.Core.Services.Interfaces;

namespace websocketschat.Web.Helpers.MessageHandler
{
    public class MessageHandler
    {
        private readonly IUserService _userService;
        public MessageHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<string> HandleAsync(string userName, string userMessage)
        {
            
            if(userMessage.StartsWith("/"))
            {
               return await CommandExecutorAsync(userName, userMessage.Remove(0, 1));
            }

            return userMessage;
        }

        private async Task<string> CommandExecutorAsync(string userName, string commandLine)
        {
            // Команды
            // /commands

            // Смена никнейма
            // /change_name=nickname
            if (commandLine.Contains("change_name="))
            {
                string newNickname = commandLine.Substring(12);

                return $"User {userName} changed nickname to {newNickname}.";
            }

            // Приватное сообщение
            // /send_to_username=nickname&message=text

            return "command " + "/" +commandLine + " is unsupported.";
        }
    }
}
