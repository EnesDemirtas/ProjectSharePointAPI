using MediatR;
using Microsoft.EntityFrameworkCore;
using PSP.Application.Enums;
using PSP.Application.Models;
using PSP.Application.Projects.Commands;
using PSP.Dal;
using PSP.Domain.Aggregates.ProjectAggregate;

namespace PSP.Application.Projects.CommandHandlers;

public class RemoveCommentFromProjectHandler : IRequestHandler<RemoveCommentFromProject, OperationResult<ProjectComment>>
{
    private readonly DataContext _ctx;
    private readonly OperationResult<ProjectComment> _result;

    public RemoveCommentFromProjectHandler(DataContext ctx)
    {
        _ctx = ctx;
        _result = new OperationResult<ProjectComment>();
    }

    public async Task<OperationResult<ProjectComment>> Handle(RemoveCommentFromProject request, CancellationToken cancellationToken)
    {
        var post = await _ctx.Projects.Include(p => p.Comments)
            .FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId, cancellationToken);

        if (post == null)
        {
            _result.AddError(ErrorCode.NotFound, ProjectErrorMessages.PostNotFound);
            return _result;
        }

        var comment = post.Comments.FirstOrDefault(c => c.CommentId == request.CommentId);
        if (comment == null)
        {
            _result.AddError(ErrorCode.NotFound, ProjectErrorMessages.PostCommentNotFound);
            return _result;
        }

        if (comment.UserProfileId != request.UserProfileId)
        {
            _result.AddError(ErrorCode.CommentRemovalNotAuthorized, ProjectErrorMessages.CommentRemovalNotAuthorized);
            return _result;
        }

        post.RemoveComment(comment);
        _ctx.Projects.Update(post);
        await _ctx.SaveChangesAsync(cancellationToken);

        _result.Payload = comment;

        return _result;
    }
}