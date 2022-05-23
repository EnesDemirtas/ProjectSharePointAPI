using MediatR;
using Microsoft.EntityFrameworkCore;
using PSP.Application.Enums;
using PSP.Application.Models;
using PSP.Application.Users.Commands;
using PSP.Dal;
using PSP.Domain.Aggregates.UserAggregate;
using PSP.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Application.Users.CommandHandlers {

    internal class UpdateUserBasicInfoHandler : IRequestHandler<UpdateUserBasicInfoCommand, OperationResult<UserProfile>> {
        private readonly DataContext _ctx;

        public UpdateUserBasicInfoHandler(DataContext ctx) {
            _ctx = ctx;
        }

        public async Task<OperationResult<UserProfile>> Handle(UpdateUserBasicInfoCommand request, CancellationToken cancellationToken) {
            var result = new OperationResult<UserProfile>();

            try {
                var userProfile = await _ctx.UserProfiles.FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId);

                if (userProfile is null) {
                    result.AddError(ErrorCode.NotFound, string.Format(UserProfileErrorMessages.UserProfileNotFound, request.UserProfileId));
                    return result;
                }

                var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName, request.UserName, request.EmailAddress);

                userProfile.UpdateBasicInfo(basicInfo);

                _ctx.UserProfiles.Update(userProfile);
                await _ctx.SaveChangesAsync();

                result.Payload = userProfile;
                return result;
            } catch (UserProfileNotValidException ex) {
                ex.ValidationErrors.ForEach(e => {
                    result.AddError(ErrorCode.ValidationError, e);
                });

                return result;
            } catch (Exception e) {
                result.AddUnknownError(e.Message);

            }

            return result;
        }
    }
}