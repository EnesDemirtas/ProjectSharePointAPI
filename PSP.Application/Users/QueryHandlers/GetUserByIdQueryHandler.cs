using MediatR;
using Microsoft.EntityFrameworkCore;
using PSP.Application.Enums;
using PSP.Application.Models;
using PSP.Application.Users.Queries;
using PSP.Dal;
using PSP.Domain.Aggregates.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Application.Users.QueryHandlers {

    internal class GetUserProfileByIdQueryHandler : IRequestHandler<GetUserProfileById, OperationResult<UserProfile>> {
        private readonly DataContext _ctx;

        public GetUserProfileByIdQueryHandler(DataContext ctx) {
            _ctx = ctx;
        }

        public async Task<OperationResult<UserProfile>> Handle(GetUserProfileById request, CancellationToken cancellationToken) {
            var result = new OperationResult<UserProfile>();
            var profile = await _ctx.UserProfiles.FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId);

            if (profile is null) {
                result.AddError(ErrorCode.NotFound, string.Format(UserProfileErrorMessages.UserProfileNotFound, request.UserProfileId));
                return result;
            }

            result.Payload = profile;
            return result;
        }
    }
}