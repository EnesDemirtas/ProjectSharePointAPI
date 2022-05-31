using MediatR;
using PSP.Application.Models;
using PSP.Domain.Aggregates.ProjectAggregate;


namespace PSP.Application.Projects.Queries
{
    public class GetProjectsByCategoryId : IRequest<OperationResult<List<Project>>>
    {
        public Guid CategoryId { get; set; }
    }
}
