using MediatR;
using Microsoft.EntityFrameworkCore;
using PSP.Application.Enums;
using PSP.Application.Models;
using PSP.Application.Projects.Queries;
using PSP.Dal;
using PSP.Domain.Aggregates.ProjectAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Application.Projects.QueryHandlers
{
    public class GetProjectsByCategoryIdHandler : IRequestHandler<GetProjectsByCategoryId, OperationResult<List<Project>>>
    {
        private readonly DataContext _ctx;

        public GetProjectsByCategoryIdHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<List<Project>>> Handle(GetProjectsByCategoryId request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<Project>>();
            var post = _ctx.Projects.Include(c => c.Category).Include(pc => pc.Comments)
                .Include(pi => pi.Interactions)
                .Where(p => p.Category.CategoryId == request.CategoryId).ToList();

            if (post is null)
            {
                result.AddError(ErrorCode.NotFound, 
                    string.Format(ProjectErrorMessages.PostNotFound, request.CategoryId));
                return result;
            }

            result.Payload = post;
            return result;
        }
    }
}
