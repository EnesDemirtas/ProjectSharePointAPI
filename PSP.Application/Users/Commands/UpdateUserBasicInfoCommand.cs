using MediatR;
using PSP.Application.Models;
using PSP.Domain.Aggregates.UserAggregate;

namespace PSP.Application.Users.Commands
{

    public class UpdateUserBasicInfoCommand : IRequest<OperationResult<UserProfile>>
    {
        public Guid UserProfileId { get; set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string UserName { get; private set; }
        public string EmailAddress { get; private set; }
    }
}