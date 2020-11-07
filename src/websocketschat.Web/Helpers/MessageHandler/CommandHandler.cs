using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace websocketschat.Web.Helpers.MessageHandler
{
    public static class CommandHandler
    {
        public static string Execute(string userMessage)
        {
            // Команды
            // /commands

            // Смена никнейма
            // /change_name=nickname

            // Приватное сообщение
            // /send_to_username=nickname&message=text

            return userMessage;
        }
    }
}
