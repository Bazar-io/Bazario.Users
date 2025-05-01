using Bazario.Users.Application.UseCases.Users.Queries.GetAllAdmins;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bazario.Users.WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            ISender sender, 
            ILogger<UsersController> logger)
        {
            _sender = sender;
            _logger = logger;
        }

        #region Queries


        [HttpGet("admins")]
        public async Task<IActionResult> GetAllAdmins(
            CancellationToken cancellationToken)
        {
            _logger.LogTrace("GetAllAdmins endpoint hit.");

            var queryResult = await _sender.Send(
                new GetAllAdminsQuery(),
                cancellationToken);

            _logger.LogTrace("Returning response for GetAllAdmins Endpoint.");

            return Ok(queryResult.Value);
        }


        #endregion
    }
}
