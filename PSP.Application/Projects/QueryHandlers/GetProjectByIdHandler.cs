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

    public class GetProjectByIdHandler : IRequestHandler<GetProjectById, OperationResult<Project>> {
        private readonly DataContext _ctx;

        public GetProjectByIdHandler(DataContext ctx) {
            _ctx = ctx;
        }

        public async Task<OperationResult<Project>> Handle(GetProjectById request, CancellationToken cancellationToken) {
            var result = new OperationResult<Project>();
            var post = await _ctx.Projects.FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId);

            if (post is null) {
                result.IsError = true;
                var error = new Error { Code = ErrorCode.NotFound, Message = $"No Post found with ID {request.ProjectId}" };
                result.Errors.Add(error);
                return result;
            }

            result.Payload = post;
            return result;
        }
    }
}