using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PSP.Application.Enums;
using PSP.Application.Identity.Commands;
using PSP.Application.Identity.Dtos;
using PSP.Application.Models;
using PSP.Dal;
using PSP.Domain.Aggregates.UserAggregate;
using System.IdentityModel.Tokens.Jwt;
using PSP.Application.Services;


namespace PSP.Application.Identity.Handlers; 

public class LoginCommandHandler : IRequestHandler<LoginCommand, OperationResult<IdentityUserProfileDto>> {
        private readonly DataContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IdentityService _identityService;
        private readonly IMapper _mapper;
        private OperationResult<IdentityUserProfileDto> _result = new();

        public LoginCommandHandler(DataContext ctx, UserManager<IdentityUser> userManager, IdentityService identityService,
            IMapper mapper) {
            _ctx = ctx;
            _userManager = userManager;
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<OperationResult<IdentityUserProfileDto>> Handle(LoginCommand request, CancellationToken cancellationToken) {
            try {
                var identityUser = await ValidateAndGetIdentityAsync(request);
                if (_result.IsError) return _result;

                var userProfile = await _ctx.UserProfiles
                    .FirstOrDefaultAsync(up => up.IdentityId == identityUser.Id, cancellationToken);

                _result.Payload = _mapper.Map<IdentityUserProfileDto>(userProfile);
                _result.Payload.UserName = identityUser.UserName;
                _result.Payload.Token = GetJwtString(identityUser, userProfile);

                return _result;
            } catch (Exception e) {
                _result.AddUnknownError(e.Message);
            }

            return _result;
        }

        private async Task<IdentityUser> ValidateAndGetIdentityAsync(LoginCommand request) {
            var identityUser = await _userManager.FindByEmailAsync(request.UserName);

            if (identityUser is null) {
                _result.AddError(ErrorCode.IdentityUserDoesNotExist, IdentityErrorMessages.NonExistentIdentityUser);
            }

            var validPassword = await _userManager.CheckPasswordAsync(identityUser, request.Password);

            if (!validPassword) {
                _result.AddError(ErrorCode.IncorrectPassword, IdentityErrorMessages.IncorrectPassword);
            }

            return identityUser;
        }

        private string GetJwtString(IdentityUser identityUser, UserProfile userProfile) {
            var claimsIdentity = new ClaimsIdentity(new Claim[] {
                        new Claim(JwtRegisteredClaimNames.Sub, identityUser.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
                        new Claim("IdentityId", identityUser.Id),
                        new Claim("UserProfileId", userProfile.UserProfileId.ToString())
                    });

            var token = _identityService.CreateSecurityToken(claimsIdentity);

            return _identityService.WriteToken(token);
        }
    }