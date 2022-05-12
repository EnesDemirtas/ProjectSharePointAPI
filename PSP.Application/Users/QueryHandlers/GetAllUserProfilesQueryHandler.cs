using MediatR;
using Microsoft.EntityFrameworkCore;
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

    internal class GetAllUserProfilesQueryHandler : IRequestHandler<GetAllUserProfiles, OperationResult<IEnumerable<UserProfile>>> {
        private readonly DataContext _ctx;

        public GetAllUserProfilesQueryHandler(DataContext ctx) {
            _ctx = ctx;
        }

        public async Task<OperationResult<IEnumerable<UserProfile>>> Handle(GetAllUserProfiles request, CancellationToken cancellationToken) {
            var result = new OperationResult<IEnumerable<UserProfile>>();
            result.Payload = await _ctx.UserProfiles.ToListAsync();
            return result;
        }
    }
}