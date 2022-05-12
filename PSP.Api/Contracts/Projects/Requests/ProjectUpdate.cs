namespace PSP.Api.Contracts.Projects.Requests {

    public class ProjectUpdate {

        [Required]
        public string Text { get; set; }
    }
}