using MediatR;
using PSP.Application.Models;
using PSP.Domain.Aggregates.ProjectAggregate;

namespace PSP.Application.Projects.Queries;

public class GetProjectInteractions : IRequest<OperationResult<List<ProjectInteraction>>>
{
    public Guid ProjectId { get; set; }
}