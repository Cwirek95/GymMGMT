using GymMGMT.Application.CQRS.Auth.Commands.ChangeRoleStatus;
using GymMGMT.Application.CQRS.Auth.Commands.CreateRole;
using GymMGMT.Application.CQRS.Auth.Commands.DeleteRole;
using GymMGMT.Application.CQRS.Auth.Commands.UpdateRole;
using GymMGMT.Application.CQRS.Auth.Queries.GetRoleDetail;
using GymMGMT.Application.CQRS.Auth.Queries.GetRolesList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymMGMT.Api.Controllers.Admin
{
    [Route("api/admin")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("[controller]")]
        public async Task<ActionResult<RolesInListViewModel>> GetAll()
        {
            var response = await _mediator.Send(new GetRolesListQuery());

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("[controller]/detail")]
        public async Task<ActionResult<RoleDetailViewModel>> Detail([FromBody] GetRoleDetailQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost("[controller]")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateRoleCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("[controller]")]
        public async Task<ActionResult> Update([FromBody] UpdateRoleCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("[controller]/changeStatus", Name = "ChangeRoleStatus")]
        public async Task<ActionResult> ChangeStatus([FromBody] ChangeRoleStatusCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpDelete("[controller]")]
        public async Task<ActionResult> Delete([FromBody] DeleteRoleCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}
