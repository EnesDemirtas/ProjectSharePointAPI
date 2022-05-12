using MediatR;
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

    public class CreateProjectHandler : IRequestHandler<CreateProject, OperationResult<Project>> {
        private readonly DataContext _ctx;

        public CreateProjectHandler(DataContext ctx) {
            _ctx = ctx;
        }

        public async Task<OperationResult<Project>> Handle(CreateProject request, CancellationToken cancellationToken) {
            var result = new OperationResult<Project>();

            try {
                var post = Project.CreateProject(request.UserProfileId, request.TextContent);
                _ctx.Projects.Add(post);
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