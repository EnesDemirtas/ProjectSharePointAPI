using MediatR;
using PSP.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSP.Application.Identity.Dtos;

namespace PSP.Application.Identity.Commands {

    public class RegisterIdentity : IRequest<OperationResult<IdentityUserProfileDto>> {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }
    }
}