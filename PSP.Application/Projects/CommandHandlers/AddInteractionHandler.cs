using MediatR;
using Microsoft.EntityFrameworkCore;
using PSP.Application.Enums;
using PSP.Application.Models;
using PSP.Application.Projects.Commands;
using PSP.Dal;
using PSP.Domain.Aggregates.ProjectAggregate;

namespace PSP.Application.Projects.CommandHandlers;

public class AddInteractionHandler : IRequestHandler<AddInteraction, OperationResult<ProjectInteraction>>
{
    private readonly DataContext _ctx;

    public AddInteractionHandler(DataContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<OperationResult<ProjectInteraction>> Handle(AddInteraction request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<ProjectInteraction>();
        try
        {
            var post = await _ctx.Projects.Include(p => p.Interactions)
                .FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId, cancellationToken);
            if (post == null)
            {
                result.AddError(ErrorCode.NotFound, ProjectErrorMessages.PostNotFound);
                return result;
            }
            var interaction = ProjectInteraction.CreateProjectInteraction(request.ProjectId, request.UserProfileId, request.Type);
            post.AddInteraction(interaction);
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