namespace PSP.Application.Projects;

public class ProjectErrorMessages
{
    public const string ProjectNotFound = "No project found with ID {0}";
    public const string ProjectDeleteNotPossible = "Only the owner of a project can delete it";
    public const string ProjectUpdateNotPossible = "Project update not possible because it's not the project owner that initiates the update";
    public const string ProjectInteractionNotFound = "Interaction not found";
    public const string InteractionRemovalNotAuthorized = "Cannot remove interaction as you are not its author";
    public const string ProjectCommentNotFound = "Comment not found";
    public const string CommentRemovalNotAuthorized = "Cannot remove content from project as you are not its author";
}