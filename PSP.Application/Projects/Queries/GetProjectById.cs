using MediatR;
using PSP.Application.Models;
using PSP.Domain.Aggregates.ProjectAggregate;

namespace PSP.Application.Projects.Queries
{

    public class GetProjectById : IRequest<OperationResult<Project>>
    {
        public Guid ProjectId { get; set; }
    }
}