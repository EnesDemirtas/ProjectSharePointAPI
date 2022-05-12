namespace PSP.Api.Contracts.Projects.Responses {

    public class ProjectResponse {
        public Guid ProjectId { get; set; }
        public Guid UserProfileId { get; set; }
        public string ProjectContent { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
    }
}