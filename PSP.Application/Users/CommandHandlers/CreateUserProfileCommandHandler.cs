using MediatR;
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

    public class CreateUserProfileCommandHandler : IRequestHandler<CreateUserProfileCommand, OperationResult<UserProfile>> {
        private readonly DataContext _ctx;

        public CreateUserProfileCommandHandler(DataContext ctx) {
            _ctx = ctx;
        }

        public async Task<OperationResult<UserProfile>> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken) {
            var result = new OperationResult<UserProfile>();

            try {
                var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName, request.UserName, request.EmailAddress);

                var userProfile = UserProfile.CreateUser(Guid.NewGuid().ToString(), basicInfo);

                _ctx.UserProfiles.Add(userProfile);
                await _ctx.SaveChangesAsync();

                result.Payload = userProfile;

                return result;
            } catch (UserProfileNotValidException ex) {
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