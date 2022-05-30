using MediatR;
using PSP.Application.Models;
using PSP.Domain.Aggregates.ProjectAggregate;

namespace PSP.Application.Projects.Queries
{

    public class GetProjectComments : IRequest<OperationResult<List<ProjectComment>>>
    {
        public Guid ProjectId { get; set; }
    }
}