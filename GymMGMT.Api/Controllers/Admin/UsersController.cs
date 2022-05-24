using GymMGMT.Application.CQRS.Auth.Commands.ChangePassword;
using GymMGMT.Application.CQRS.Auth.Commands.ChangeUserRole;
using GymMGMT.Application.CQRS.Auth.Commands.ChangeUserStatus;
using GymMGMT.Application.CQRS.Auth.Commands.DeleteUser;
using GymMGMT.Application.CQRS.Auth.Queries.GetUserDetail;
using GymMGMT.Application.CQRS.Auth.Queries.GetUsersList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymMGMT.Api.Controllers.Admin
{
    [Route("api/admin")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("[controller]")]
        public async Task<ActionResult<UsersInListViewModel>> GetAll()
        {
            var response = await _mediator.Send(new GetUsersListQuery());

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("[controller]/detail")]
        public async Task<ActionResult<UserDetailViewModel>> Detail([FromBody] GetUserDetailQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("[controller]/changeStatus", Name = "ChangeUserStatus")]
        public async Task<ActionResult> ChangeStatus([FromBody] ChangeUserStatusCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("[controller]/changePassword", Name = "ChangeUserRole")]
        public async Task<ActionResult> ChangeUserRole([FromBody] ChangeUserRoleCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("[controller]/changePassword", Name = "ChangeUserPassword")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpDelete("[controller]")]
        public async Task<ActionResult> Delete([FromBody] DeleteUserCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}