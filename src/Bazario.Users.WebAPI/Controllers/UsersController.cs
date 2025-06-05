using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Bazario.AspNetCore.Shared.Domain.Common.Users.Roles;
using Bazario.Users.Application.UseCases.Users.Commands.BanAdmin;
using Bazario.Users.Application.UseCases.Users.Commands.BanUser;
using Bazario.Users.Application.UseCases.Users.Commands.DeleteAdmin;
using Bazario.Users.Application.UseCases.Users.DTO;
using Bazario.Users.Application.UseCases.Users.Queries.GetAdminById;
using Bazario.Users.Application.UseCases.Users.Queries.GetAllAdmins;
using Bazario.Users.WebAPI.Factories;
using Microsoft.AspNetCore.Mvc;

namespace Bazario.Users.WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public sealed class UsersController(
        #region Handlers
        IQueryHandler<GetAllAdminsQuery, IEnumerable<UserResponse>> getAllAdminsQueryHandler,
        IQueryHandler<GetAdminByIdQuery, UserResponse> getAdminByIdQueryHandler,
        ICommandHandler<DeleteAdminCommand> deleteAdminCommandHandler,
        ICommandHandler<BanAdminCommand> banAdminCommandHandler,
        ICommandHandler<BanUserCommand> banUserCommandHandler,
        #endregion
        ProblemDetailsFactory problemDetailsFactory) : ControllerBase
    {
        #region Queries


        [HttpGet("admins")]
        public async Task<IActionResult> GetAllAdmins(
            CancellationToken cancellationToken)
        {
            var queryResult = await getAllAdminsQueryHandler.Handle(
                new GetAllAdminsQuery(),
                cancellationToken);

            return queryResult.IsSuccess ? Ok(queryResult.Value) : problemDetailsFactory.GetProblemDetails(queryResult);
        }

        [HttpGet("admins/{id:guid}")]
        public async Task<IActionResult> GetAdminById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var queryResult = await getAdminByIdQueryHandler.Handle(
                new GetAdminByIdQuery(id),
                cancellationToken);

            return queryResult.IsSuccess ? Ok(queryResult.Value) : problemDetailsFactory.GetProblemDetails(queryResult);
        }


        #endregion

        #region Commands


        [HttpDelete("admins/{id:guid}")]
        public async Task<IActionResult> DeleteAdmin(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var commandResult = await deleteAdminCommandHandler.Handle(
                new DeleteAdminCommand(id),
                cancellationToken);

            return commandResult.IsSuccess ? NoContent() : problemDetailsFactory.GetProblemDetails(commandResult);
        }

        [HttpPost("admins/ban")]
        public async Task<IActionResult> BanAdmin(
            [FromBody] BanAdminCommand command,
            CancellationToken cancellationToken)
        {
            var commandResult = await banAdminCommandHandler.Handle(
                command,
                cancellationToken);

            return commandResult.IsSuccess ? NoContent() : problemDetailsFactory.GetProblemDetails(commandResult);
        }

        [HttpPost("ban")]
        public async Task<IActionResult> BanUser(
            [FromBody] BanUserCommand command,
            CancellationToken cancellationToken)
        {
            var commandResult = await banUserCommandHandler.Handle(
                command,
                cancellationToken);

            return commandResult.IsSuccess ? NoContent() : problemDetailsFactory.GetProblemDetails(commandResult);
        }


        #endregion
    }
}
