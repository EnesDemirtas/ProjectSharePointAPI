using AutoMapper;
using PSP.Api.Contracts.Projects.Responses;
using PSP.Domain.Aggregates.ProjectAggregate;

namespace PSP.Api.MappingProfiles {

    public class ProjectMappings : Profile {

        public ProjectMappings() {
            CreateMap<Project, ProjectResponse>();
            CreateMap<ProjectComment, ProjectCommentResponse>();
        }
    }
}