namespace PSP.Api.Contracts.Projects.Responses
{

    public class ProjectResponse
    {
        public Guid ProjectId { get; set; }
        public Guid UserProfileId { get; set; }
        public string ProjectContent { get; set; }
        public string ProjectName { get; set; }
        public List<Domain.Aggregates.CategoryAggregate.Category> Categories { get; set; }
        public List<ProjectComment> Comments { get; set; }
        public List<ProjectInteraction> Interactions { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
    }
}