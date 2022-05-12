using AutoMapper;
using PSP.Api.Contracts.Identity;
using PSP.Application.Identity.Commands;

namespace PSP.Api.MappingProfiles {

    public class IdentityMappings : Profile {

        public IdentityMappings() {
            CreateMap<UserRegistration, RegisterIdentity>();
        }
    }
}