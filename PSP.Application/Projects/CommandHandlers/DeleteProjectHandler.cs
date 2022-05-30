using MediatR;
using Microsoft.EntityFrameworkCore;
using PSP.Application.Enums;
using PSP.Application.Models;
using PSP.Application.Projects.Commands;
using PSP.Dal;
using PSP.Domain.Aggregates.ProjectAggregate;

namespace PSP.Application.Projects.CommandHandlers
{

    public class DeleteProjectHandler : IRequestHandler<DeleteProject, OperationResult<Project>>
    {
        private readonly DataContext _ctx;

        public DeleteProjectHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<Project>> Handle(DeleteProject request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Project>();

            try
            {
                var project = await _ctx.Projects.FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId);

                if (project is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ProjectErrorMessages.PostNotFound, request.ProjectId));
                    return result;
                }

                if (project.UserProfileId != request.UserProfileId)
                {
                    result.AddError(ErrorCode.PostDeleteNotPossible, ProjectErrorMessages.PostDeleteNotPossible);
                    return result;
                }

                _ctx.Projects.Remove(project);
                await _ctx.SaveChangesAsync();
                result.Payload = project;
            }
            catch (Exception e)
            {
                result.AddUnknownError(e.Message);

            }

            return result;
        }
    }
}