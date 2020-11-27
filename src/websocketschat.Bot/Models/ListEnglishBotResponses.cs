using System;
using websocketschat.Bot.Abstracts;

namespace websocketschat.Bot
{
    class ListEnglishBotResponses : ListResponses
    {
        public override string Greeting()
        {
            string[] response = new string[] {
                "Hello!!!",
                "Hello, glad to see You.",
                "Hi!",
            };

            return response[RandomIndex(response.Length)];
        }

        public override string QuestionResolve()
        {
            string[] response = new string[] {
                "Well!",
                "Well, do you?",
                "Well",
                "Wonderful!",
                "Fine, and you?",
                "Good",
            };

            return response[RandomIndex(response.Length)];
        }

        public override string Time()
        {
            string[] response = new string[] {
                $"Now {DateTime.Now.ToShortTimeString()} time",
                $"Mister, now {DateTime.Now.ToShortTimeString()} time",
            };

            return response[RandomIndex(response.Length)];
        }

        public override string Farewell()
        {
            string[] response = new string[] {
                "Bye",
                "Goodbye!!!",
                "See you soon!",
            };

            return response[RandomIndex(response.Length)];
        }

    }
}
