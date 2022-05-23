namespace PSP.Api.Contracts.Projects.Responses; 

public class ProjectInteraction {
    public Guid InteractionId { get; set; }
    public string Type { get; set; }
    public InteractionUser Author { get; set; }
}