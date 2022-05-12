namespace PSP.Api.MappingProfiles {

    public class IdentityMappings : Profile {

        public IdentityMappings() {
            CreateMap<UserRegistration, RegisterIdentity>();
        }
    }
}