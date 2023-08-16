using AutoMapper;
using TestTaskForInforce.Data.Entities;
using TestTaskForInforce.Models.Responses;

namespace TestTaskForInforce.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, UserResponse>()
                .ForMember(destination => destination.Role, opt => opt.MapFrom(src => src.Role.Name));
        }
    }
}
