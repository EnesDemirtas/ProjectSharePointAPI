﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PSP.Api.Contracts.UserProfile.Requests;
using PSP.Api.Contracts.UserProfile.Responses;
using PSP.Api.Filters;
using PSP.Application.Users.Commands;
using PSP.Application.Users.Queries;

namespace PSP.Api.Controllers {

    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class UserProfilesController : BaseController {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserProfilesController(IMediator mediator, IMapper mapper) {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProfiles() {
            var query = new GetAllUserProfiles();
            var response = await _mediator.Send(query);
            var profiles = _mapper.Map<List<UserProfileResponse>>(response.Payload);
            return Ok(profiles);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateUserProfile([FromBody] UserProfileCreateUpdate profile) {
            var command = _mapper.Map<CreateUserProfileCommand>(profile);
            var response = await _mediator.Send(command);
            var userProfile = _mapper.Map<UserProfileResponse>(response.Payload);

            return CreatedAtAction(nameof(GetUserProfileById), new { id = userProfile.UserProfileId }, userProfile);
        }

        [Route(ApiRoutes.UserProfiles.IdRoute)]
        [HttpGet]
        [ValidateGuid("id")]
        public async Task<IActionResult> GetUserProfileById(string id) {
            var query = new GetUserProfileById { UserProfileId = Guid.Parse(id) };
            var response = await _mediator.Send(query);

            if (response.IsError) return HandleErrorResponse(response.Errors);

            var userProfile = _mapper.Map<UserProfileResponse>(response.Payload);
            return Ok(userProfile);
        }

        [HttpPatch]
        [Route(ApiRoutes.UserProfiles.IdRoute)]
        [ValidateModel]
        [ValidateGuid("id")]
        public async Task<IActionResult> UpdateUserProfile(string id, UserProfileCreateUpdate updatedProfile) {
            var command = _mapper.Map<UpdateUserBasicInfoCommand>(updatedProfile);
            command.UserProfileId = Guid.Parse(id);
            var response = await _mediator.Send(command);
            return response.IsError ? HandleErrorResponse(response.Errors) : NoContent();
        }

        [HttpDelete]
        [Route(ApiRoutes.UserProfiles.IdRoute)]
        [ValidateGuid("id")]
        public async Task<IActionResult> DeleteUserProfile(string id) {
            var command = new DeleteUserProfileCommand() { UserProfileId = Guid.Parse(id) };
            var response = await _mediator.Send(command);

            return response.IsError ? HandleErrorResponse(response.Errors) : NoContent();
        }
    }
}