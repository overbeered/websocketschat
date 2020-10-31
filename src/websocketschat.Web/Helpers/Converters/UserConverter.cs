using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbModels = websocketschat.Database.Models;
using CoreModels = websocketschat.Core.Models;

namespace websocketschat.Web.Helpers.Converters
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
          //  CreateMap<PostUserDto, UserModel>().ReverseMap();
          //  CreateMap<GetUserDto, UserModel>().ReverseMap();
          //  CreateMap<LoginUserDto, UserModel>().ReverseMap();

            CreateMap<DbModels.User, CoreModels.User>().ReverseMap();
        }
    }
}
