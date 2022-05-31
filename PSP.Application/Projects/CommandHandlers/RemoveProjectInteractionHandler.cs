using MediatR;
using Microsoft.EntityFrameworkCore;
using PSP.Application.Enums;
using PSP.Application.Models;
using PSP.Application.Projects.Commands;
using PSP.Dal;
using PSP.Domain.Aggregates.ProjectAggregate;

namespace PSP.Application.Projects.CommandHandlers;

public class RemoveProjectInteractionHandler : IRequestHandler<RemoveProjectInteraction, OperationResult<ProjectInteraction>>
{
    private readonly DataContext _ctx;

    public RemoveProjectInteractionHandler(DataContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<OperationResult<ProjectInteraction>> Handle(RemoveProjectInteraction request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<ProjectInteraction>();
        try
        {
            var post = await _ctx.Projects.Include(p => p.Interactions)
                .FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId);

            if (post is null)
            {
                result.AddError(ErrorCode.NotFound, string.Format(ProjectErrorMessages.ProjectNotFound, request.ProjectId));
                return result;
            }

            var interaction = post.Interactions.FirstOrDefault(i => i.InteractionId == request.InteractionId);
            if (interaction is null)
            {
                result.AddError(ErrorCode.NotFound, ProjectErrorMessages.ProjectInteractionNotFound);
                return result;
            }

            if (interaction.UserProfileId != request.UserProfileId)
            {
                result.AddError(ErrorCode.InteractionRemovalNotAuthorized, ProjectErrorMessages.InteractionRemovalNotAuthorized);
                return result;
            }

            post.RemoveInteraction(interaction);
            _ctx.Projects.Update(post);
            await _ctx.SaveChangesAsync(cancellationToken);

            result.Payload = interaction;
        }
        catch (Exception e)
        {
            result.AddUnknownError(e.Message);
        }

        return result;
    }
}