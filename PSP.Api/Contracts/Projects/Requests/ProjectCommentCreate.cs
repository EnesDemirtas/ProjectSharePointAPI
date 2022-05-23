namespace PSP.Api.Contracts.Projects.Requests {

    public class ProjectCommentCreate {

        [Required]
        public string Text { get; set; }
        
    }
}