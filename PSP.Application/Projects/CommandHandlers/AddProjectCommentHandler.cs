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

    public class AddProjectCommentHandler : IRequestHandler<AddProjectComment, OperationResult<ProjectComment>> {
        private readonly DataContext _ctx;

        public AddProjectCommentHandler(DataContext ctx) {
            _ctx = ctx;
        }

        public async Task<OperationResult<ProjectComment>> Handle(AddProjectComment request, CancellationToken cancellationToken) {
            var result = new OperationResult<ProjectComment>();

            try {
                var post = await _ctx.Projects.FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId);

                if (post is null) {
                    result.IsError = true;
                    var error = new Error { Code = ErrorCode.NotFound, Message = $"No Post found with ID {request.ProjectId}" };
                    result.Errors.Add(error);
                    return result;
                }

                var comment = ProjectComment.CreateProjectComment(request.ProjectId, request.CommentText, request.UserProfileId);

                post.AddProjectComment(comment);

                _ctx.Projects.Update(post);
                await _ctx.SaveChangesAsync();
                result.Payload = comment;
            } catch (ProjectCommentNotValidException ex) {
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