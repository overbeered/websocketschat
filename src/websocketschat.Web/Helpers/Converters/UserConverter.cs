using AutoMapper;
using websocketschat.Datatransfer.HttpDto;
using CoreModels = websocketschat.Core.Models;
using DbModels = websocketschat.Database.Models;

namespace websocketschat.Web.Helpers.Converters
{
    public class UserConverter : Profile
    {
        public UserConverter()
        {
            CreateMap<PostUserDto, CoreModels.User>().ReverseMap();
            CreateMap<GetUserDto, CoreModels.User>().ReverseMap();

            CreateMap<DbModels.User, CoreModels.User>().ReverseMap();
        }
    }
}
