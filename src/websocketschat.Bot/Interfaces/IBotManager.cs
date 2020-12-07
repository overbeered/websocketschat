namespace websocketschat.Bot.Interfaces
{
    /// <summary>
    /// Бот
    /// </summary>
    public interface IBotManager
    {
        /// <summary>
        /// Обработчик
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Отевет бота</returns>
        public string Process(string command);
    }
}
