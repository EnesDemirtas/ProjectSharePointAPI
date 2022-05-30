using MediatR;
using PSP.Application.Models;
using PSP.Domain.Aggregates.ProjectAggregate;

namespace PSP.Application.Projects.Commands
{

    public class AddProjectComment : IRequest<OperationResult<ProjectComment>>
    {
        public Guid ProjectId { get; set; }
        public Guid UserProfileId { get; set; }
        public string CommentText { get; set; }
    }
}