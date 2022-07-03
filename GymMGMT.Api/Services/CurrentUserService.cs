using GymMGMT.Application.Security.Contracts;
using GymMGMT.Domain.Entities;
using System.Security.Claims;

namespace GymMGMT.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public ClaimsPrincipal User => _contextAccessor.HttpContext?.User;
        public string? UserId =>
            User is null ? null : User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
        public string? Email =>
            User is null ? null : User.FindFirst(c => c.Type == ClaimTypes.Email).Value;
        public string? Role =>
            User is null ? null : User.FindFirst(c => c.Type == ClaimTypes.Role).Value;
    }
}