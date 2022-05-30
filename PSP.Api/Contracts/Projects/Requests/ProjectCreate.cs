namespace PSP.Api.Contracts.Projects.Requests
{

    public class ProjectCreate
    {

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        [StringLength(1000)]
        public string TextContent { get; set; }
    }
}