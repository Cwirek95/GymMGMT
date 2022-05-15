using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Models.Responses;
using GymMGMT.Application.Security.Contracts;
using GymMGMT.Application.Security.Models;
using GymMGMT.Domain.Entities;
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

        public AuthenticationService(JwtSettings jwtSettings, IUserRepository userRepository)
        {
            _jwtSettings = jwtSettings;
            _userRepository = userRepository;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateUserAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> GetUserRoleAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> ChangeUserRoleAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        private async Task<JwtSecurityToken> GenerateToken(User user)
        {
            var claims = new List<Claim>()
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.Name)
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
