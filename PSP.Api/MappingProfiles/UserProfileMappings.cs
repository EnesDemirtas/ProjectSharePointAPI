namespace PSP.Api.MappingProfiles {

    public class UserProfileMappings : Profile {

        public UserProfileMappings() {
            CreateMap<UserProfileCreateUpdate, CreateUserProfileCommand>();
            CreateMap<UserProfileCreateUpdate, UpdateUserBasicInfoCommand>();
            CreateMap<UserProfile, UserProfileResponse>();
            CreateMap<BasicInfo, BasicInformation>();
        }
    }
}