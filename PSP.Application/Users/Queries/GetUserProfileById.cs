using MediatR;
using PSP.Application.Models;
using PSP.Domain.Aggregates.UserAggregate;

namespace PSP.Application.Users.Queries
{

    public class GetUserProfileById : IRequest<OperationResult<UserProfile>>
    {
        public Guid UserProfileId { get; set; }
    }
}