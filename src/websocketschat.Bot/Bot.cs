using System.Text.RegularExpressions;
using websocketschat.Bot.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;
using websocketschat.Bot.Parsing.Response;
using System;

namespace websocketschat.Bot
{

    public class Bot : IBotManager
    {
        private string _name;
        private string _password;
        byte[] _byteArrayBot;
        public Bot(string name, string password)
        {
            _name = name;
            _password = password;
            _byteArrayBot = System.Text.Encoding.UTF8.GetBytes($"username={_name}&password={_password}");
        }

        /// <summary>
        /// Обработчик сообщения на русском 
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Отевет бота</returns>
        private string ProcessRussian(string command)
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

        /// <summary>
        /// Обработчик сообщения на английском
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Отевет бота</returns>
        private string ProcessEnglish(string command)
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

        /// <summary>
        /// Обработчик
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Отевет бота</returns>
        public string Process(string command)
        {
            return Regex.IsMatch(command, "[а-яА-ЯеЁ]") ? ProcessRussian(command) : ProcessEnglish(command);
        }

        /// <summary>
        /// регистрация бота
        /// </summary>
        /// <param name="urlRegister"></param>
        /// <returns>статус регистрации</returns>
        public async Task<int> RegisterBotAsync(string urlRegister)
        {

            WebRequest request = WebRequest.Create(urlRegister);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = _byteArrayBot.Length;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(_byteArrayBot, 0, _byteArrayBot.Length);
            }


            try
            {
                WebResponse response = await request.GetResponseAsync();
                return 201;
            }
            catch (WebException e)
            {
                var webResponse = (HttpWebResponse)e.Response;
                return (int)webResponse.StatusCode;
            }
        }
       
        /// <summary>
        /// Авторизация бота 
        /// </summary>
        /// <param name="urlToken"></param>
        /// <param name="postQueryAfterGetToken"></param>
        /// <param name="webToken"></param>
        /// <returns></returns>
        public async Task AuthBotAsync(string urlToken, string postQueryAfterGetToken, string webToken)
        {
            try
            {
                Root responseObject = await AuthBotUrlTokenAsync(urlToken);
                Parsing.ResponseOtDeda.Root root = await AuthBotPostQueryAfterGetTokenAsync(postQueryAfterGetToken, responseObject);
                HubConnection hub = new HubConnectionBuilder()
                    .WithUrl(webToken, options =>
                    {
                        options.AccessTokenProvider = () => Task.FromResult(responseObject.access_token);
                    })
                    .Build();
                await hub.StartAsync();
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Первый пост запрос на сервер для получения токена
        /// </summary>
        /// <param name="urlToken"></param>
        /// <returns></returns>
        private async Task<Root> AuthBotUrlTokenAsync(string urlToken)
        {
            WebRequest request = WebRequest.Create(urlToken);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = _byteArrayBot.Length;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(_byteArrayBot, 0, _byteArrayBot.Length);
            }

            WebResponse response = await request.GetResponseAsync();

            Root responseObject;

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return responseObject = JsonSerializer.Deserialize<Root>(reader.ReadToEnd());
                }
            }

        }

        /// <summary>
        /// Второй пост запрос на сервер для получения токена
        /// </summary>
        /// <param name="postQueryAfterGetToken"></param>
        /// <param name="responseObject"></param>
        /// <returns></returns>
        private async Task<Parsing.ResponseOtDeda.Root> AuthBotPostQueryAfterGetTokenAsync(string postQueryAfterGetToken, Root responseObject)
        {
            WebRequest request = WebRequest.Create(postQueryAfterGetToken);
            request.Method = "POST";
            request.ContentType = "text/plain;charset=UTF-8";
            request.Headers.Add("Authorization", "Bearer " + responseObject.access_token);
            request.ContentLength = _byteArrayBot.Length;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(_byteArrayBot, 0, _byteArrayBot.Length);
            }

            WebResponse response = await request.GetResponseAsync();
            
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return JsonSerializer.Deserialize<Parsing.ResponseOtDeda.Root>(reader.ReadToEnd());
                }
            }

        }
    }
}
