using AutoMapper;
using CoreModels = websocketschat.Core.Models;
using DbModels = websocketschat.Database.Models;

namespace websocketschat.Web.Helpers.Converters
{
    public class RoleConverter : Profile
    {
        public RoleConverter()
        {
            CreateMap<DbModels.Role, CoreModels.Role>().ReverseMap();
        }
    }
}
