using MediatR;
using PSP.Application.Models;
using PSP.Domain.Aggregates.ProjectAggregate;

namespace PSP.Application.Projects.Commands; 

public class AddInteraction : IRequest<OperationResult<ProjectInteraction>> {
    public Guid ProjectId { get; set; }
    public Guid UserProfileId { get; set; }
    public InteractionType Type { get; set; }
}