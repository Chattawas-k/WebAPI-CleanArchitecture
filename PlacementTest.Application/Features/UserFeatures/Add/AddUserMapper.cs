using AutoMapper;
using PlacementTest.Domain.Entities;

namespace PlacementTest.Application.Features.UserFeatures.Add
{
    public sealed class AddUserMapper : Profile
    {
        public AddUserMapper()
        {
            CreateMap<User, AddUserResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
        }
    }
}