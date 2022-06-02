using GymMGMT.Application.CQRS.Auth.Commands.CreateUser;
using GymMGMT.Application.CQRS.Auth.Commands.SignInUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymMGMT.Api.Controllers
{
    [Route("api")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost("register")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateUserCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("login")]
        public async Task<ActionResult<Guid>> LogIn([FromBody] SignInUserCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}
