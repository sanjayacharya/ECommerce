using AutoMapper;

namespace ECommerce.Api.Users.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Db.User,Models.User>().ReverseMap();
        }
    }
}
