using MediatR;
using PSP.Application.Models;
using PSP.Domain.Aggregates.UserAggregate;

namespace PSP.Application.Users.Queries
{

    public class GetAllUserProfiles : IRequest<OperationResult<IEnumerable<UserProfile>>>
    {
    }
}