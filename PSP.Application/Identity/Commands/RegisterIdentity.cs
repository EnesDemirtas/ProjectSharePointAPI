using MediatR;
using PSP.Application.Identity.Dtos;
using PSP.Application.Models;

namespace PSP.Application.Identity.Commands
{

    public class RegisterIdentity : IRequest<OperationResult<IdentityUserProfileDto>>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }
    }
}