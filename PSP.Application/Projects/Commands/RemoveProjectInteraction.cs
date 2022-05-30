using MediatR;
using PSP.Application.Models;
using PSP.Domain.Aggregates.ProjectAggregate;

namespace PSP.Application.Projects.Commands;

public class RemoveProjectInteraction : IRequest<OperationResult<ProjectInteraction>>
{
    public Guid ProjectId { get; set; }
    public Guid InteractionId { get; set; }
    public Guid UserProfileId { get; set; }
}