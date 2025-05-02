using Bazario.Users.Application.UseCases.Users.Commands.BanAdmin;
using Bazario.Users.Application.UseCases.Users.Commands.BanUser;
using Bazario.Users.Application.UseCases.Users.Commands.DeleteAdmin;
using Bazario.Users.Application.UseCases.Users.Queries.GetAdminById;
using Bazario.Users.Application.UseCases.Users.Queries.GetAllAdmins;
using Bazario.Users.WebAPI.Factories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bazario.Users.WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ProblemDetailsFactory _problemDetailsFactory;

        public UsersController(
            ISender sender,
            ProblemDetailsFactory problemDetailsFactory)
        {
            _sender = sender;
            _problemDetailsFactory = problemDetailsFactory;
        }

        #region Queries


        [HttpGet("admins")]
        public async Task<IActionResult> GetAllAdmins(
            CancellationToken cancellationToken)
        {
            var queryResult = await _sender.Send(
                new GetAllAdminsQuery(),
                cancellationToken);

            return queryResult.IsSuccess ? Ok(queryResult.Value) : _problemDetailsFactory.GetProblemDetails(queryResult);
        }

        [HttpGet("admins/{id:guid}")]
        public async Task<IActionResult> GetAdminById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var queryResult = await _sender.Send(
                new GetAdminByIdQuery(id),
                cancellationToken);

            return queryResult.IsSuccess ? Ok(queryResult.Value) : _problemDetailsFactory.GetProblemDetails(queryResult);
        }


        #endregion

        #region Commands


        [HttpDelete("admins/{id:guid}")]
        public async Task<IActionResult> DeleteAdmin(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var commandResult = await _sender.Send(
                new DeleteAdminCommand(id),
                cancellationToken);

            return commandResult.IsSuccess ? NoContent() : _problemDetailsFactory.GetProblemDetails(commandResult);
        }

        [HttpPost("admins/ban")]
        public async Task<IActionResult> BanAdmin(
            [FromBody] BanAdminCommand command,
            CancellationToken cancellationToken)
        {
            var commandResult = await _sender.Send(
                request: command,
                cancellationToken);

            return commandResult.IsSuccess ? NoContent() : _problemDetailsFactory.GetProblemDetails(commandResult);
        }

        [HttpPost("ban")]
        public async Task<IActionResult> BanUser(
            [FromBody] BanUserCommand command,
            CancellationToken cancellationToken)
        {
            var commandResult = await _sender.Send(
                request: command,
                cancellationToken);

            return commandResult.IsSuccess ? NoContent() : _problemDetailsFactory.GetProblemDetails(commandResult);
        }


        #endregion
    }
}
