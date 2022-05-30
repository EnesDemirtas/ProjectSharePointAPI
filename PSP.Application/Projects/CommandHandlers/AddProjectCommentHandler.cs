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

    public class AddProjectCommentHandler : IRequestHandler<AddProjectComment, OperationResult<ProjectComment>>
    {
        private readonly DataContext _ctx;

        public AddProjectCommentHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<ProjectComment>> Handle(AddProjectComment request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<ProjectComment>();

            try
            {
                var post = await _ctx.Projects.FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId);

                if (post is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ProjectErrorMessages.PostNotFound, request.ProjectId));
                    return result;
                }

                var comment = ProjectComment.CreateProjectComment(request.ProjectId, request.CommentText, request.UserProfileId);

                post.AddProjectComment(comment);

                _ctx.Projects.Update(post);
                await _ctx.SaveChangesAsync();
                result.Payload = comment;
            }
            catch (ProjectCommentNotValidException ex)
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