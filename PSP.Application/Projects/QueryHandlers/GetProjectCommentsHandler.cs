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

namespace PSP.Application.Projects.QueryHandlers {

    public class GetProjectCommentsHandler : IRequestHandler<GetProjectComments, OperationResult<List<ProjectComment>>> {
        private readonly DataContext _ctx;

        public GetProjectCommentsHandler(DataContext ctx) {
            _ctx = ctx;
        }

        public async Task<OperationResult<List<ProjectComment>>> Handle(GetProjectComments request, CancellationToken cancellationToken) {
            var result = new OperationResult<List<ProjectComment>>();
            try {
                var post = await _ctx.Projects.Include(p => p.Comments).
                    FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId);

                result.Payload = post.Comments.ToList();
            } catch (Exception e) {
                var error = new Error { Code = ErrorCode.UnknownError, Message = $"{e.Message}" };
                result.IsError = true;
                result.Errors.Add(error);
            }

            return result;
        }
    }
}