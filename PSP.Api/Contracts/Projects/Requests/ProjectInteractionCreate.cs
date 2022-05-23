namespace PSP.Api.Contracts.Projects.Requests; 

public class ProjectInteractionCreate {
    [Required]
    public InteractionType Type { get; set; }
}