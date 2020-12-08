using System.Threading.Tasks;

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

        /// <summary>
        /// регистрация бота
        /// </summary>
        /// <param name="urlRegister"></param>
        /// <returns>статус регистрации</returns>
        public Task<int> RegisterBotAsync(string urlRegister);

        /// <summary>
        /// Авторизация бота 
        /// </summary>
        /// <param name="urlToken"></param>
        /// <param name="postQueryAfterGetToken"></param>
        /// <param name="webToken"></param>
        /// <returns></returns>
        public Task AuthBotAsync(string urlToken, string postQueryAfterGetToken, string webToken);
    }
}
