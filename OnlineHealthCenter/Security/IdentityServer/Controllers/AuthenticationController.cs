using IdentityServer.DTOs;
using IdentityServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;
        private readonly ILogger<AuthenticationController> logger;

        public AuthenticationController(IAuthenticationService authenticationService, ILogger<AuthenticationController> logger)
        {
            this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
    }
}
