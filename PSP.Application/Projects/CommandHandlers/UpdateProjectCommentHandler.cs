using MediatR;
using Microsoft.EntityFrameworkCore;
using PSP.Application.Enums;
using PSP.Application.Models;
using PSP.Application.Projects.Commands;
using PSP.Dal;
using PSP.Domain.Aggregates.ProjectAggregate;

namespace PSP.Application.Projects.CommandHandlers;

public class UpdateProjectCommentHandler : IRequestHandler<UpdateProjectComment, OperationResult<ProjectComment>>
{
    private readonly DataContext _ctx;
    private readonly OperationResult<ProjectComment> _result;

    public UpdateProjectCommentHandler(DataContext ctx)
    {
        _ctx = ctx;
        _result = new OperationResult<ProjectComment>();
    }

    public async Task<OperationResult<ProjectComment>> Handle(UpdateProjectComment request, CancellationToken cancellationToken)
    {
        var post = await _ctx.Projects.Include(p => p.Comments)
            .FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId, cancellationToken);

        if (post == null)
        {
            _result.AddError(ErrorCode.NotFound, ProjectErrorMessages.ProjectNotFound);
            return _result;
        }

        var comment = post.Comments.FirstOrDefault(c => c.CommentId == request.CommentId);
        if (comment == null)
        {
            _result.AddError(ErrorCode.NotFound, ProjectErrorMessages.ProjectCommentNotFound);
            return _result;
        }

        if (comment.UserProfileId != request.UserProfileId)
        {
            _result.AddError(ErrorCode.CommentRemovalNotAuthorized, ProjectErrorMessages.CommentRemovalNotAuthorized);
            return _result;
        }

        comment.UpdateCommentText(request.UpdatedText);
        _ctx.Projects.Update(post);
        await _ctx.SaveChangesAsync(cancellationToken);

        return _result;
    }
}