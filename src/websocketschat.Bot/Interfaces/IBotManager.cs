namespace websocketschat.Bot.Interfaces
{
    /// <summary>
    /// Бот
    /// </summary>
    public interface IBotManager
    {
        /// <summary>
        /// Обработчик сообщения на русском 
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Отевет бота</returns>
        public string ProcessRussian(string command);
        /// <summary>
        /// Обработчик сообщения на английском
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Отевет бота</returns>
        public string ProcessEnglish(string command);
    }
}
