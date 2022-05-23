namespace PSP.Api.Contracts.Projects.Requests {

    public class ProjectCreate {
        
        [Required]
        [StringLength(1000)]
        public string TextContent { get; set; }
    }
}