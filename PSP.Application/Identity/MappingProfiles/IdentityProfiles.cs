using AutoMapper;
using PSP.Application.Identity.Dtos;
using PSP.Domain.Aggregates.UserAggregate;

namespace PSP.Application.Identity.MappingProfiles;

public class IdentityProfiles : Profile
{

    public IdentityProfiles()
    {
        CreateMap<UserProfile, IdentityUserProfileDto>()
            .ForMember(dest => dest.EmailAddress, opt
                => opt.MapFrom(src => src.BasicInfo.EmailAddress))
            .ForMember(dest => dest.FirstName, opt
                => opt.MapFrom(src => src.BasicInfo.FirstName))
            .ForMember(dest => dest.LastName, opt
                => opt.MapFrom(src => src.BasicInfo.LastName))
            .ForMember(dest => dest.UserName, opt
                => opt.MapFrom(src => src.BasicInfo.UserName))

            ;
    }
}