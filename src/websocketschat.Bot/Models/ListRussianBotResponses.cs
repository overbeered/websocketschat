using System;
using websocketschat.Bot.Abstracts;

namespace websocketschat.Bot
{
    class ListRussianBotResponses : ListResponses
    {

        public override string Greeting()
        {
            string[] response = new string[] {
                "Привет!!!",
                "Приветствую, рад Вам.",
                "Здравствуйте!",
            };

            return response[RandomIndex(response.Length)];
        }

        public override string QuestionResolve()
        {

            string[] response = new string[] {
                "Хорошо!",
                "Хорошо, а у вас?",
                "Хорошо",
                "Замечательно!",
                "Замечательно, а у вас?",
                "Замечательно",
                "Отлично!",
                "Отлично, а у вас?",
                "Отлично"
            };

            return response[RandomIndex(response.Length)];
        }

        public override string Time()
        {
            string[] response = new string[] {
                $"Сейчас {DateTime.Now.ToShortTimeString()} времени",
                $"Хозяин, сейчас {DateTime.Now.ToShortTimeString()} времени",
            };

            return response[RandomIndex(response.Length)];
        }

        public override string Farewell()
        {
            string[] response = new string[] {
                "Пока",
                "До свидания!!!",
                "До скорой встречи!",
            };

            return response[RandomIndex(response.Length)];
        }
    }
}
