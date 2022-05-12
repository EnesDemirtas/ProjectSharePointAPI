namespace PSP.Api.MappingProfiles {

    public class ProjectMappings : Profile {

        public ProjectMappings() {
            CreateMap<Project, ProjectResponse>();
            CreateMap<ProjectComment, ProjectCommentResponse>();
        }
    }
}