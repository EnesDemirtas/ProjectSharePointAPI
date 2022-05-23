namespace PSP.Api.Contracts.Projects.Responses {

    public class ProjectCommentResponse {
        public Guid CommentId { get; set; }
        public string Text { get; set; }
        public string UserProfileId { get; set; }
    }
}