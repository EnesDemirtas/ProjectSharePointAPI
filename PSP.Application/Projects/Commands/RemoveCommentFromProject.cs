using MediatR;
using PSP.Application.Models;
using PSP.Domain.Aggregates.ProjectAggregate;

namespace PSP.Application.Projects.Commands;

public class RemoveCommentFromProject : IRequest<OperationResult<ProjectComment>>
{
    public Guid UserProfileId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid CommentId { get; set; }
}