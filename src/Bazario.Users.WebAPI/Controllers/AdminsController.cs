﻿using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Bazario.AspNetCore.Shared.Api.Factories;
using Bazario.Users.Application.UseCases.Users.Commands.BanAdmin;
using Bazario.Users.Application.UseCases.Users.Commands.DeleteAdmin;
using Bazario.Users.Application.UseCases.Users.DTO;
using Bazario.Users.Application.UseCases.Users.Queries.GetAdminById;
using Bazario.Users.Application.UseCases.Users.Queries.GetAllAdmins;
using Bazario.Users.Application.UseCases.Users.Queries.GetCurrentAdmin;
using Microsoft.AspNetCore.Mvc;

namespace Bazario.Users.WebAPI.Controllers
{
    [Route("api/admins")]
    [ApiController]
    public class AdminsController(
        ProblemDetailsFactory problemDetailsFactory) : ControllerBase
    {
        #region Queries


        [HttpGet]
        public async Task<IActionResult> GetAllAdmins(
            [FromServices] IQueryHandler<GetAllAdminsQuery, IEnumerable<UserResponse>> queryHandler,
            CancellationToken cancellationToken)
        {
            var queryResult = await queryHandler.Handle(
                new GetAllAdminsQuery(),
                cancellationToken);

            return queryResult.IsSuccess ? Ok(queryResult.Value) : problemDetailsFactory.GetProblemDetails(queryResult);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAdminById(
            [FromServices] IQueryHandler<GetAdminByIdQuery, UserResponse> queryHandler,
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var queryResult = await queryHandler.Handle(
                new GetAdminByIdQuery(id),
                cancellationToken);

            return queryResult.IsSuccess ? Ok(queryResult.Value) : problemDetailsFactory.GetProblemDetails(queryResult);
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentAdmin(
            [FromServices] IQueryHandler<GetCurrentAdminQuery, UserResponse> queryHandler,
            CancellationToken cancellationToken)
        {
            var queryResult = await queryHandler.Handle(
                new GetCurrentAdminQuery(),
                cancellationToken);

            return queryResult.IsSuccess ? Ok(queryResult.Value) : problemDetailsFactory.GetProblemDetails(queryResult);
        }


        #endregion

        #region Commands


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAdmin(
            [FromServices] ICommandHandler<DeleteAdminCommand> commandHandler,
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var commandResult = await commandHandler.Handle(
                new DeleteAdminCommand(id),
                cancellationToken);

            return commandResult.IsSuccess ? NoContent() : problemDetailsFactory.GetProblemDetails(commandResult);
        }

        [HttpPost("ban")]
        public async Task<IActionResult> BanAdmin(
            [FromServices] ICommandHandler<BanAdminCommand> commandHandler,
            [FromBody] BanAdminCommand command,
            CancellationToken cancellationToken)
        {
            var commandResult = await commandHandler.Handle(
                command,
                cancellationToken);

            return commandResult.IsSuccess ? NoContent() : problemDetailsFactory.GetProblemDetails(commandResult);
        }


        #endregion
    }
}
