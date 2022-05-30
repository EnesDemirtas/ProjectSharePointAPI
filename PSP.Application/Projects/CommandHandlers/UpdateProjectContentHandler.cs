using MediatR;
using Microsoft.EntityFrameworkCore;
using PSP.Application.Enums;
using PSP.Application.Models;
using PSP.Application.Projects.Commands;
using PSP.Dal;
using PSP.Domain.Aggregates.ProjectAggregate;
using PSP.Domain.Exceptions;

namespace PSP.Application.Projects.CommandHandlers
{

    public class UpdateProjectContentHandler : IRequestHandler<UpdateProjectContent, OperationResult<Project>>
    {
        private readonly DataContext _ctx;

        public UpdateProjectContentHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<Project>> Handle(UpdateProjectContent request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Project>();

            try
            {
                var post = await _ctx.Projects.FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId);

                if (post is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ProjectErrorMessages.PostNotFound, request.ProjectId));
                    return result;
                }

                if (post.UserProfileId != request.UserProfileId)
                {
                    result.AddError(ErrorCode.PostUpdateNotPossible, ProjectErrorMessages.PostUpdateNotPossible);
                    return result;
                }

                post.UpdateProjectContent(request.NewText);
                await _ctx.SaveChangesAsync();
                result.Payload = post;
            }
            catch (ProjectNotValidException ex)
            {
                ex.ValidationErrors.ForEach(e =>
                {
                    result.AddError(ErrorCode.ValidationError, e);
                });
            }
            catch (Exception e)
            {
                result.AddUnknownError(e.Message);

            }

            return result;
        }
    }
}