using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Bazario.AspNetCore.Shared.Authorization.Attributes;
using Bazario.AspNetCore.Shared.Domain.Common.Users.Roles;
using Bazario.Users.Application.UseCases.Users.Commands.BanUser;
using Bazario.Users.WebAPI.Factories;
using Microsoft.AspNetCore.Mvc;

namespace Bazario.Users.WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public sealed class UsersController(
    #region Handlers
        ICommandHandler<BanUserCommand> banUserCommandHandler,
    #endregion
        ProblemDetailsFactory problemDetailsFactory) : ControllerBase
    {
        #region Queries

        #endregion

        #region Commands


        [HasRole(Role.Admin)]
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
