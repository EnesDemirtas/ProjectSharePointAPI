using MediatR;
using PSP.Application.Identity.Dtos;
using PSP.Application.Models;

namespace PSP.Application.Identity.Commands; 

    public class LoginCommand : IRequest<OperationResult<IdentityUserProfileDto>> {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
