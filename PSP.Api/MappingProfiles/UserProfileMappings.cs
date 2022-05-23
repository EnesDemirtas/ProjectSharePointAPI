namespace PSP.Api.MappingProfiles {

    public class UserProfileMappings : Profile {

        public UserProfileMappings() {
            CreateMap<UserProfileCreateUpdate, UpdateUserBasicInfoCommand>();
            CreateMap<UserProfile, UserProfileResponse>();
            CreateMap<BasicInfo, BasicInformation>();
            CreateMap<UserProfile, InteractionUser>()
                .ForMember(dest 
                        => dest.FullName, opt 
                        => opt.MapFrom(src 
                        => src.BasicInfo.FirstName + " " + src.BasicInfo.LastName));
        }
    }
}