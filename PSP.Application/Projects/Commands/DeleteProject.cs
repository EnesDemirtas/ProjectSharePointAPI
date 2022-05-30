using MediatR;
using PSP.Application.Models;
using PSP.Domain.Aggregates.ProjectAggregate;

namespace PSP.Application.Projects.Commands
{

    public class DeleteProject : IRequest<OperationResult<Project>>
    {
        public Guid ProjectId { get; set; }
        public Guid UserProfileId { get; set; }

    }
}