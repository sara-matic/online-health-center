using AutoMapper;
using IdentityServer.DTOs;
using IdentityServer.Entities;
using IdentityServer.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers.Base
{
    public class RegistrationControllerBase<T> : ControllerBase
    {
        protected readonly IIdentityRepository repository;
        protected readonly IMapper mapper;
        protected readonly ILogger<T> logger;

        public RegistrationControllerBase(IIdentityRepository repository, IMapper mapper, ILogger<T> logger)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected async Task<IActionResult> RegisterNewUserWithRoles(NewUserDTO newUserDTO, IEnumerable<string> roles)
        {
            var user = mapper.Map<User>(newUserDTO);
            var result = await repository.CreateUser(user, newUserDTO.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            logger.LogInformation($"Successfully registered user: {user.UserName}.");

            foreach (var role in roles)
            {
                if (await repository.AddRoleToUser(user, role))
                {
                    logger.LogInformation($"Added role {role} to user {user.UserName}.");
                }
                else
                {
                    logger.LogInformation($"Role {role} does not exist.");
                }
            }

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
