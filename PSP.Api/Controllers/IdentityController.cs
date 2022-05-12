using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PSP.Api.Contracts.Identity;
using PSP.Api.Filters;
using PSP.Application.Identity.Commands;

namespace PSP.Api.Controllers {

    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class IdentityController : BaseController {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public IdentityController(IMediator mediator, IMapper mapper) {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Route(ApiRoutes.Identity.Registration)]
        [ValidateModel]
        public async Task<IActionResult> Register(UserRegistration registration) {
            var command = _mapper.Map<RegisterIdentity>(registration);
            var result = await _mediator.Send(command);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            var authenticationResult = new AuthenticationResult { Token = result.Payload };

            return Ok(authenticationResult);
        }
    }
}