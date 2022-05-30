using MediatR;
using PSP.Application.Models;
using PSP.Domain.Aggregates.ProjectAggregate;

namespace PSP.Application.Projects.Commands
{

    public class CreateProject : IRequest<OperationResult<Project>>
    {
        public Guid UserProfileId { get; set; }
        public Guid CategoryId { get; set; }
        public string ProjectName { get; set; }
        public string TextContent { get; set; }
    }
}