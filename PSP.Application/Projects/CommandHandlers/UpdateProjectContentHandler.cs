using MediatR;
using Microsoft.EntityFrameworkCore;
using PSP.Application.Enums;
using PSP.Application.Models;
using PSP.Application.Projects.Commands;
using PSP.Dal;
using PSP.Domain.Aggregates.ProjectAggregate;
using PSP.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Application.Projects.CommandHandlers {

    public class UpdateProjectContentHandler : IRequestHandler<UpdateProjectContent, OperationResult<Project>> {
        private readonly DataContext _ctx;

        public UpdateProjectContentHandler(DataContext ctx) {
            _ctx = ctx;
        }

        public async Task<OperationResult<Project>> Handle(UpdateProjectContent request, CancellationToken cancellationToken) {
            var result = new OperationResult<Project>();

            try {
                var post = await _ctx.Projects.FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId);

                if (post is null) {
                    result.IsError = true;
                    var error = new Error { Code = ErrorCode.NotFound, Message = $"No Post found with ID {request.ProjectId}" };
                    result.Errors.Add(error);
                    return result;
                }

                post.UpdateProjectContent(request.NewText);
                await _ctx.SaveChangesAsync();
                result.Payload = post;
            } catch (ProjectNotValidException ex) {
                result.IsError = true;
                ex.ValidationErrors.ForEach(e => {
                    var error = new Error {
                        Code = ErrorCode.ValidationError,
                        Message = $"{ex.Message}"
                    };
                    result.Errors.Add(error);
                });
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