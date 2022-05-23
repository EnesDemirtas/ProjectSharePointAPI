using ProjectInteraction = PSP.Domain.Aggregates.ProjectAggregate.ProjectInteraction;

namespace PSP.Api.MappingProfiles {

    public class ProjectMappings : Profile {

        public ProjectMappings() {
            CreateMap<Project, ProjectResponse>();
            CreateMap<ProjectComment, ProjectCommentResponse>();
            CreateMap<Domain.Aggregates.ProjectAggregate.ProjectInteraction, Api.Contracts.Projects.Responses.ProjectInteraction>()
                .ForMember(dest 
                    => dest.Type, opt 
                    => opt.MapFrom(src
                    => src.InteractionType.ToString()))
                .ForMember(dest 
                    => dest.Author, opt 
                    => opt.MapFrom(src 
                    => src.UserProfile));
        }
    }
}