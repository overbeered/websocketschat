using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace websocketschat.Web.Providers
{
    public class NameUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            // Поменять на Guid чтобы починить приватные сообщения.
            // Поскольку IdentifierId сменное поле, после изменения никнейма мы не можем отправим ему сообщение!
            return connection.User?.FindFirstValue("Guid");
        }
    }
}
