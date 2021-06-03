using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationOffice.Sso.Core.Models;
using ApplicationOffice.Sso.Core.Tools;
using ApplicationOffice.Sso.Data;
using ApplicationOffice.Sso.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ApplicationOffice.Sso.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AoIdentityUser> _userManager;
        private readonly SsoDbContext _db;
        private readonly IOptions<DataProtectionTokenProviderOptions> _dataProtectionTokenOptions;

        public UserService(
            UserManager<AoIdentityUser> userManager,
            SsoDbContext db,
            IOptions<DataProtectionTokenProviderOptions> dataProtectionTokenOptions)
        {
            _userManager = userManager;
            _db = db;
            _dataProtectionTokenOptions = dataProtectionTokenOptions;
        }

        public async Task Add(AddUserDto request)
        {
            // todo add db-level validation and move this check there
            // (so it won't be possible to have invalid entity in db
            if (string.IsNullOrEmpty(request.PhoneNumber) && string.IsNullOrEmpty(request.Email))
                throw new ValidationException("Phone number and email cannot be both empty.");

            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x =>
                    x.NormalizedUserName == _userManager.NormalizeName(request.UserName) ||
                    request.PhoneNumber != null && x.PhoneNumber == request.PhoneNumber ||
                    request.Email != null && x.NormalizedEmail == _userManager.NormalizeEmail(request.Email));
            if (existingUser is not null)
                throw new ValidationException("User already exists");

            var newUser = new AoIdentityUser
            {
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                PhoneNumberConfirmed = false,
                Email = request.Email,
                EmailConfirmed = false,
            };

            await using var transaction = await _db.Database.BeginTransactionAsync();

            var result = await _userManager.CreateAsync(newUser, request.Password);
            result.ValidateOrThrow();

            if (request.Claims.Any())
            {
                var claims = request.Claims.Select(x => new Claim(x.Key, x.Value));
                result = await _userManager.AddClaimsAsync(newUser, claims);
                result.ValidateOrThrow();
            }

            await transaction.CommitAsync();
        }

        public async Task ChangePassword(string userId, string currentPassword, string newPassword)
        {
            var user = await GetUserOrThrow(userId);

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            result.ValidateOrThrow();
        }

        public async Task<string> GeneratePasswordResetToken(string userId)
        {
            var user = await GetUserOrThrow(userId);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return token;
        }

        public async Task ResetPassword(string userId, string resetPassToken, string newPassword)
        {
            var user = await GetUserOrThrow(userId);
            var result = await _userManager.ResetPasswordAsync(user, resetPassToken, newPassword);
            result.ValidateOrThrow();
        }

        public async Task<(string token, DateTime tokenExpirationTime)> GenerateConfirmPhoneToken(string userId)
        {
            var user = await GetUserOrThrow(userId);

            if (string.IsNullOrEmpty(user.PhoneNumber))
                throw new ValidationException("Phone number is not specified.");

            if (user.PhoneNumberConfirmed)
                throw new ValidationException("Phone number already confirmed");

            // todo get this value from somewhere
            var tokenExpirationTime = DateTime.UtcNow.AddMinutes(3);

            var token = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);

            return (token, tokenExpirationTime);
        }

        public async Task<string> ConfirmPhone(string userId, string token)
        {
            var user = await GetUserOrThrow(userId);

            if (string.IsNullOrEmpty(user.PhoneNumber))
                throw new ValidationException("Phone number is not specified.");

            if (user.PhoneNumberConfirmed)
                throw new ValidationException("Phone number is already confirmed.");

            var result = await _userManager.ChangePhoneNumberAsync(user, user.PhoneNumber, token);

            result.ValidateOrThrow();

            return user.PhoneNumber;
        }

        public async Task<(string token, DateTime tokenExpirationTime)> GenerateConfirmEmailToken(string userId)
        {
            var user = await GetUserOrThrow(userId);

            if (string.IsNullOrEmpty(user.Email))
                throw new ValidationException("Email is not specified.");

            if (user.EmailConfirmed)
                throw new ValidationException("Email is already confirmed.");

            var tokenExpirationTime = DateTime.UtcNow.Add(_dataProtectionTokenOptions.Value.TokenLifespan);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return (token, tokenExpirationTime);
        }

        public async Task<string> ConfirmEmail(string userId, string token)
        {
            var user = await GetUserOrThrow(userId);

            if (user.EmailConfirmed)
                throw new ValidationException("Phone number is already confirmed.");

            var result = await _userManager.ConfirmEmailAsync(user, token);

            result.ValidateOrThrow();

            return user.Email;
        }

        private async Task<AoIdentityUser> GetUserOrThrow(string userId) =>
            await _userManager.FindByIdAsync(userId)
                ?? throw new ValidationException($"User with id:{userId} not found");
    }
}