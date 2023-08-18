using AutoMapper;
using TestTaskForInforce.Data.Entities;
using TestTaskForInforce.Models.Responses;

namespace TestTaskForInforce.Mapping
{
    public class UrlProfile : Profile
    {
        public UrlProfile()
        {
            CreateMap<UrlEntity, UrlResponse>()
                .ForMember(destination => destination.User, opt => opt.MapFrom(src => src.User));
        }
    }
}
