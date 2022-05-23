﻿using MediatR;
using PSP.Application.Enums;
using PSP.Application.Models;
using PSP.Application.Projects.Commands;
using PSP.Dal;
using PSP.Domain.Aggregates.ProjectAggregate;
using PSP.Domain.Exceptions;

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
                ex.ValidationErrors.ForEach(e => {
                    result.AddError(ErrorCode.ValidationError, e);
                });
            } catch (Exception e) {
                result.AddUnknownError(e.Message);
            }

            return result;
        }
    }
}