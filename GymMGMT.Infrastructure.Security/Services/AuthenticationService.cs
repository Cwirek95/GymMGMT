using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Exceptions;
using GymMGMT.Application.Models.Responses;
using GymMGMT.Application.Security.Contracts;
using GymMGMT.Application.Security.Exceptions;
using GymMGMT.Application.Security.Models;
using GymMGMT.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GymMGMT.Infrastructure.Security.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public AuthenticationService(IOptions<JwtSettings> jwtSettings, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _jwtSettings = jwtSettings.Value;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailWithDetailsAsync(email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                throw new UnauthorizedException("Invalid credentials. Please try again");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            AuthenticationResponse response = new AuthenticationResponse()
            {
                Id = user.Id,
                Email = user.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            };

            return response;
        }

        public async Task<User> CreateUserAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailWithDetailsAsync(email);
            if (user != null)
                throw new ConflictException("User with this email is already exist");

            var newUser = new User()
            {
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                RegisteredAt = DateTimeOffset.Now,
                Status = true
            };

            newUser = await _userRepository.AddAsync(newUser);

            return newUser;
        }

        public async Task<string> GetUserRoleAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdWithDetailsAsync(userId);
            var role = user.Role.Name;

            return role;
        }

        public async Task ChangeUserPasswordAsync(Guid userId, string oldPassword, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.Password))
                throw new BadRequestException("Invalid credentials", "Invalid credentials. Please try again.");

            if (newPassword.Equals(oldPassword))
                throw new ConflictException("The new password cannot be the same as the current password");

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _userRepository.UpdateAsync(user);
        }

        public async Task ChangeUserRoleAsync(Guid userId, Guid newRoleId)
        {
            var user = await _userRepository.GetByIdWithDetailsAsync(userId);
            if(user == null)
                throw new NotFoundException(nameof(User), userId);

            var role = await _roleRepository.GetByIdAsync(newRoleId);
            if(role == null)
                throw new NotFoundException(nameof(Role), newRoleId);

            if (!user.RoleId.Equals(newRoleId))
            {
                user.RoleId = newRoleId;
                await _userRepository.UpdateAsync(user);
            }
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdWithDetailsAsync(userId);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), userId);
            }
            await _userRepository.DeleteAsync(user);
        }

        private async Task<JwtSecurityToken> GenerateToken(User user)
        {
            var claims = new List<Claim>()
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
