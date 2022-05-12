using MediatR;
using PSP.Application.Models;
using PSP.Domain.Aggregates.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Application.Users.Commands {

    public class DeleteUserProfileCommand : IRequest<OperationResult<UserProfile>> {
        public Guid UserProfileId { get; set; }
    }
}