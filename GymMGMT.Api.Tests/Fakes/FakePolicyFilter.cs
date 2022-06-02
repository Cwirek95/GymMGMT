using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace GymMGMT.Api.Tests.Fakes
{
    public class FakePolicyFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var claimsPrincipal = new ClaimsPrincipal();

            claimsPrincipal.AddIdentity(new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Email, "admin@email.com"),
                    new Claim(ClaimTypes.Role, "Admin")
                }));

            context.HttpContext.User = claimsPrincipal;

            await next();
        }
    }
}
