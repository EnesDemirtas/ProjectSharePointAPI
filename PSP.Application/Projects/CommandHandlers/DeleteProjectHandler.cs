using MediatR;
using Microsoft.EntityFrameworkCore;
using PSP.Application.Enums;
using PSP.Application.Models;
using PSP.Application.Projects.Commands;
using PSP.Dal;
using PSP.Domain.Aggregates.ProjectAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Application.Projects.CommandHandlers {

    public class DeleteProjectHandler : IRequestHandler<DeleteProject, OperationResult<Project>> {
        private readonly DataContext _ctx;

        public DeleteProjectHandler(DataContext ctx) {
            _ctx = ctx;
        }

        public async Task<OperationResult<Project>> Handle(DeleteProject request, CancellationToken cancellationToken) {
            var result = new OperationResult<Project>();

            try {
                var project = await _ctx.Projects.FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId);

                if (project is null) {
                    result.IsError = true;
                    var error = new Error { Code = ErrorCode.NotFound, Message = $"No Project found with ID {request.ProjectId}" };
                    result.Errors.Add(error);
                    return result;
                }

                _ctx.Projects.Remove(project);
                await _ctx.SaveChangesAsync();
                result.Payload = project;
            } catch (Exception e) {
                var error = new Error {
                    Code = ErrorCode.UnknownError,
                    Message = $"{e.Message}"
                };
                result.IsError = true;
                result.Errors.Add(error);
            }

            return result;
        }
    }
}