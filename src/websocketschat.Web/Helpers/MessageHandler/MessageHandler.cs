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

        public async Task<Tuple<bool,string>> HandleAsync(string userName, string userMessage)
        {
            Tuple<bool, string> answerToFront = new Tuple<bool, string>(false, userMessage);
            
            if(userMessage.StartsWith("/"))
            {
               string finallMessage =  await CommandExecutorAsync(userName, userMessage.Remove(0, 1));
               answerToFront = new Tuple<bool, string>(true, finallMessage);
            }

            return answerToFront;
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

                if(newNickname == string.Empty || newNickname == null)
                {
                    return $"User {userName} tried to change username to \'{newNickname}\' with failure.";
                }

                var userWithSameUsername = await _userService.GetUserAsync(newNickname);

                if(userWithSameUsername == null)
                {
                    var user = await _userService.GetUserAsync(userName);

                    user.Username = newNickname;

                    var updatedUser = await _userService.UpdateUserAsync(user);

                    return $"User {userName} changed nickname to {updatedUser.Username}.";
                }
               
                return $"User {userName} tried to change nickname to {newNickname} but user with this nickname already exist.";
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
