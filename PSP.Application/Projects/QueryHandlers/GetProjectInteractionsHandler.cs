using MediatR;
using Microsoft.EntityFrameworkCore;
using PSP.Application.Enums;
using PSP.Application.Models;
using PSP.Application.Projects.Queries;
using PSP.Dal;
using PSP.Domain.Aggregates.ProjectAggregate;

namespace PSP.Application.Projects.QueryHandlers;

public class GetPostInteractionsHandler : IRequestHandler<GetProjectInteractions, OperationResult<List<ProjectInteraction>>>
{
    private readonly DataContext _ctx;

    public GetPostInteractionsHandler(DataContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<OperationResult<List<ProjectInteraction>>> Handle(GetProjectInteractions request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<List<ProjectInteraction>>();

        try
        {
            var post = await _ctx.Projects.Include(p => p.Interactions)
                .ThenInclude(i => i.UserProfile)
                .FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId, cancellationToken);
            if (post == null)
            {
                result.AddError(ErrorCode.NotFound, ProjectErrorMessages.ProjectNotFound);
                return result;
            }

            result.Payload = post.Interactions.ToList();
        }
        catch (Exception e)
        {
            result.AddUnknownError(e.Message);
        }

        return result;

    }
}