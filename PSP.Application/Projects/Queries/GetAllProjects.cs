using MediatR;
using PSP.Application.Models;
using PSP.Domain.Aggregates.ProjectAggregate;

namespace PSP.Application.Projects.Queries
{

    public class GetAllProjects : IRequest<OperationResult<List<Project>>>
    {
    }
}