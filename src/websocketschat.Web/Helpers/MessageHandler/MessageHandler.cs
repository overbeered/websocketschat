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
            // В будущем хранить команды в бд и выводить список команд из бд.
            if(commandLine.ToLower().Contains("commands"))
            {
                return "/change_name=newName - change nickname if new nickname is free to pick.\n" +
                       "/send_to=username - sends private message to user if exists.\n" +
                       "/commands - shows stored commands.";
            }

            // Смена никнейма
            // /change_name=nickname
            if (commandLine.ToLower().Contains("change_name="))
            {
                string newNickname = commandLine.Substring(12);

                var user = await _userService.GetUserAsync(userName);
               
                return $"User {userName} changed nickname to {newNickname}.";
            }

            // Приватное сообщение
            // /send_to=username&message=text
            if (commandLine.ToLower().StartsWith("send_to="))
            {
                string newNickname = commandLine.Substring(12);

                return $"User {userName} changed nickname to {newNickname}.";
            }

            return "command " + "/" +commandLine + " is unsupported.";
        }
    }
}
