using MediatR;
using PSP.Application.Models;
using PSP.Domain.Aggregates.ProjectAggregate;

namespace PSP.Application.Projects.Commands
{

    public class UpdateProjectContent : IRequest<OperationResult<Project>>
    {
        public string NewText { get; set; }
        public Guid ProjectId { get; set; }
        public Guid UserProfileId { get; set; }

    }
}