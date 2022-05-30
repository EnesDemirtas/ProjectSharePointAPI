using PSP.Application.Identity.Dtos;

namespace PSP.Api.MappingProfiles
{

    public class IdentityMappings : Profile
    {

        public IdentityMappings()
        {
            CreateMap<UserRegistration, RegisterIdentity>();
            CreateMap<Login, LoginCommand>();
            CreateMap<IdentityUserProfileDto, IdentityUserProfile>();
        }
    }
}