using MediatR;
using Microsoft.EntityFrameworkCore;
using PSP.Application.Models;
using PSP.Application.Projects.Queries;
using PSP.Dal;
using PSP.Domain.Aggregates.ProjectAggregate;

namespace PSP.Application.Projects.QueryHandlers
{

    public class GetAllProjectsHandler : IRequestHandler<GetAllProjects, OperationResult<List<Project>>>
    {
        private readonly DataContext _ctx;

        public GetAllProjectsHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<List<Project>>> Handle(GetAllProjects request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<Project>>();

            try
            {
                var posts = await _ctx.Projects.Include(c => c.Category).
                    Include(pc => pc.Comments).Include(pi => pi.Interactions).ToListAsync();
                result.Payload = posts;
            }
            catch (Exception e)
            {
                result.AddUnknownError(e.Message);

            }

            return result;
        }
    }
}