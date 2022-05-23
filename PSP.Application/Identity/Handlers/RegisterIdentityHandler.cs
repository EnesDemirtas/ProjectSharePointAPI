using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PSP.Application.Enums;
using PSP.Application.Identity.Commands;
using PSP.Application.Models;
using PSP.Application.Options;
using PSP.Dal;
using PSP.Domain.Aggregates.UserAggregate;
using PSP.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using PSP.Application.Identity.Dtos;
using PSP.Application.Services;

namespace PSP.Application.Identity.Handlers {

    public class RegisterIdentityHandler : IRequestHandler<RegisterIdentity, OperationResult<IdentityUserProfileDto>> {
        private readonly DataContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IdentityService _identityService;
        private OperationResult<IdentityUserProfileDto> _result = new();
        private readonly IMapper _mapper;

        public RegisterIdentityHandler(DataContext ctx, UserManager<IdentityUser> userManager, IdentityService identityService,
            IMapper mapper) {
            _ctx = ctx;
            _userManager = userManager;
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<OperationResult<IdentityUserProfileDto>> Handle(RegisterIdentity request, CancellationToken cancellationToken) {
            try {
                await ValidateIdentityDoesNotExist(request);
                if (_result.IsError) return _result;

                await using var transaction = await this._ctx.Database.BeginTransactionAsync(cancellationToken);
                var identity = await CreateIdentityUserAsync(request, transaction);

                if (_result.IsError) return _result;

                var profile = await CreateUserProfileAsync(request, transaction, identity);
                await transaction.CommitAsync(cancellationToken);

                _result.Payload = _mapper.Map<IdentityUserProfileDto>(profile);

                _result.Payload.UserName = identity.UserName;
                _result.Payload.Token = GetJwtString(identity, profile);
                return _result;
            } catch (UserProfileNotValidException ex) {
                ex.ValidationErrors.ForEach(e => {
                    _result.AddError(ErrorCode.ValidationError, e);
                });
            } catch (Exception e) {
                _result.AddUnknownError(e.Message);
            }

            return _result;
        }

        private async Task ValidateIdentityDoesNotExist(RegisterIdentity request) {
            var existingIdentity = await _userManager.FindByEmailAsync(request.EmailAddress);

            if (existingIdentity != null) {
                _result.AddError(ErrorCode.IdentityUserAlreadyExists, IdentityErrorMessages.IdentityUserAlreadyExist);
            }
        }

        private async Task<IdentityUser> CreateIdentityUserAsync(RegisterIdentity request, IDbContextTransaction transaction) {
            var identity = new IdentityUser { Email = request.EmailAddress, UserName = request.UserName };
            var createdIdentity = await _userManager.CreateAsync(identity, request.Password);

            if (!createdIdentity.Succeeded) {
                await transaction.RollbackAsync();

                foreach (var identityError in createdIdentity.Errors) {
                    _result.AddError(ErrorCode.IdentityCreationFailed, identityError.Description);
                }
            }

            return identity;
        }

        private async Task<UserProfile> CreateUserProfileAsync(RegisterIdentity request,
            IDbContextTransaction transaction, IdentityUser identity) {
            try {
                var profileInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName, request.UserName,
request.EmailAddress);

                var profile = UserProfile.CreateUser(identity.Id, profileInfo);
                _ctx.UserProfiles.Add(profile);
                await _ctx.SaveChangesAsync();
                return profile;
            } catch (Exception) {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private string GetJwtString(IdentityUser identity, UserProfile profile) {
            var claimsIdentity = new ClaimsIdentity(new Claim[] {
                        new Claim(JwtRegisteredClaimNames.Sub, identity.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, identity.Email),
                        new Claim("IdentityId", identity.Id),
                        new Claim("UserProfileId", profile.UserProfileId.ToString())
                    });

            var token = _identityService.CreateSecurityToken(claimsIdentity);

            return _identityService.WriteToken(token);
        }
    }
}