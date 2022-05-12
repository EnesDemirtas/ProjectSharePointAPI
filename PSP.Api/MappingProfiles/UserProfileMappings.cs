using AutoMapper;
using PSP.Api.Contracts.UserProfile.Requests;
using PSP.Api.Contracts.UserProfile.Responses;
using PSP.Application.Users.Commands;
using PSP.Domain.Aggregates.UserAggregate;

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