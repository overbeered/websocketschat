using System;

namespace websocketschat.Bot.Abstracts
{
    abstract class ListResponses
    {
        /// <summary>
        /// Рандомный индекс
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        protected int RandomIndex(int maxInterval)
        {
            Random random = new Random();

            return random.Next(0, maxInterval);

        }

        /// <summary>
        /// Приветсвие
        /// </summary>
        /// <returns></returns>
        public abstract string Greeting();
        
        /// <summary>
        /// Вопрос "как дела"
        /// </summary>
        /// <returns></returns>
        public abstract string QuestionResolve();

        /// <summary>
        /// Время
        /// </summary>
        /// <returns></returns>
        public abstract string Time();

        /// <summary>
        /// Время
        /// </summary>
        /// <returns></returns>
        public abstract string Farewell();

    }
}
