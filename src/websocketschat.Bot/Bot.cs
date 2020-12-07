using websocketschat.Bot.Interfaces;

namespace websocketschat.Bot
{

    public class Bot : IBotManager
    {

        public string ProcessRussian(string command)
        {
            ListRussianBotResponses listRussianResponses = new ListRussianBotResponses();
            command = command.ToLower();

            if (command.IndexOf("привет") != -1 || command.IndexOf("здравств") != -1)
            {
                return listRussianResponses.Greeting();
            }
            else if (command.IndexOf("как дела") != -1 || command.IndexOf("как ты") != -1)
            {
                return listRussianResponses.QuestionResolve();
            }
            else if (command.IndexOf("время") != -1)
            {
                return listRussianResponses.Time();
            }
            else if (command.IndexOf("пока") != -1 || command.IndexOf("до свидания") != -1)
            {
                return listRussianResponses.Farewell();
            }
            else if (command.IndexOf("посоветуй трек") != -1)
            {
                return listRussianResponses.RecommendTrack();
            }
            else if (command.IndexOf("посоветуй фильм") != -1)
            {
                return listRussianResponses.RecommendTrack();
            }


            return "Я вас не понимаю";
        }

        public string ProcessEnglish(string command)
        {
            ListEnglishBotResponses listEnglishResponses = new ListEnglishBotResponses();
            command = command.ToLower();

            if (command.IndexOf("hi") != -1 || command.IndexOf("hello") != -1)
            {
                return listEnglishResponses.Greeting();
            }
            else if (command.IndexOf("how are you") != -1)
            {
                return listEnglishResponses.QuestionResolve();
            }
            else if (command.IndexOf("time") != -1)
            {
                return listEnglishResponses.Time();
            }
            else if (command.IndexOf("bye") != -1 || command.IndexOf("goodbye") != -1)
            {
                return listEnglishResponses.Farewell();
            }
            else if (command.IndexOf("recommend a track") != -1)
            {
                return listEnglishResponses.RecommendTrack();
            }
            else if (command.IndexOf("recommend a movie") != -1)
            {
                return listEnglishResponses.RecommendTrack();
            }


            return "I don't understand you";
        }
    }
}
