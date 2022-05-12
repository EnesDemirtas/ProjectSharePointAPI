using MediatR;
using PSP.Application.Models;
using PSP.Domain.Aggregates.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Application.Users.Queries {

    public class GetAllUserProfiles : IRequest<OperationResult<IEnumerable<UserProfile>>> {
    }
}