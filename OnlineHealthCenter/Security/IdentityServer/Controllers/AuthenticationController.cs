using IdentityServer.DTOs;
using IdentityServer.Repositories.Interfaces;
using IdentityServer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;
        private readonly ILogger<AuthenticationController> logger;
        private readonly IIdentityRepository repository;

        public AuthenticationController(IAuthenticationService authenticationService, ILogger<AuthenticationController> logger, IIdentityRepository repository)
        {
            this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(AuthenticationModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] UserCredentialsDTO userCredentials)
        {
            var user = await this.authenticationService.ValidateUser(userCredentials);
            if (user == null)
            {
                this.logger.LogWarning($"{nameof(Login)}: Authentication failed. Wrong username or password.");
                return Unauthorized();
            }

            return Ok(await this.authenticationService.CreateAuthenticationModel(user));
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(AuthenticationModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AuthenticationModel>> Refresh([FromBody] RefreshTokenModel refreshTokenModel)
        {
            var user = await this.repository.GetUserByUsername(refreshTokenModel.UserName);
            if (user == null)
            {
                this.logger.LogWarning($"{nameof(Refresh)}: Refreshing token failed. Unknown username {refreshTokenModel.UserName}.");
                return Unauthorized();
            }

            var refreshToken = user.RefreshTokens.FirstOrDefault(r => r.Token == refreshTokenModel.RefreshToken);
            if (refreshToken == null)
            {
                this.logger.LogWarning($"{nameof(Refresh)}: Refreshing token failed. The refresh token is not found.");
                return Unauthorized();
            }

            if (refreshToken.ExpiryTime < DateTime.Now)
            {
                this.logger.LogWarning($"{nameof(Refresh)}: Refreshing token failed. The refresh token is not valid.");
                return Unauthorized();
            }

            return Ok(await this.authenticationService.CreateAuthenticationModel(user));
        }

        [Authorize]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenModel refreshTokenModel)
        {
            if (User.FindFirst(ClaimTypes.Name).Value != refreshTokenModel.UserName)
            {
                this.logger.LogWarning($"{nameof(Logout)}: Logout failed. The sent username does not match the name of the user trying to logout.");
                return Forbid();
            }

            var user = await this.repository.GetUserByUsername(refreshTokenModel.UserName);
            if (user == null)
            {
                this.logger.LogWarning($"{nameof(Logout)}: Logout failed. Unknown username {refreshTokenModel.UserName}.");
                return Unauthorized();
            }

            await this.authenticationService.RemoveRefreshToken(user, refreshTokenModel.RefreshToken);

            return Accepted();
        }
    }
}
