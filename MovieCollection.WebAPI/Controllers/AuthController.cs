using MovieCollection.Core.DTOs;
using MovieCollection.Core.Exceptions;
using MovieCollection.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCollection.UserAdministration.Domain.Entities;
using MovieCollection.UserAdministration.Domain.Infrastructure;
using MovieCollection.UserAdministration.Domain.Ports.Incoming.Commands;
using MovieCollection.UserAdministration.Domain.Ports.Incoming.Queries;
using MovieCollection.UserAdministration.Domain.Ports.Incoming.Commands.Results;
using MovieCollection.WebAPI.Authorization;
using MovieCollection.WebAPI.Exceptions;
using System.Net;
using MovieCollection.UserAdministration.Domain.DTOs;

namespace MovieCollection.WebAPI.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly ICommandDispatcher _userAdminCommandDispatcher;
        private readonly IUserQueries _userQueries;

        public AuthController(IHttpContextAccessor accessor, ICommandDispatcher userAdminCommandDispatcher, IUserQueries userQueries) : base(accessor)
        {
            _userAdminCommandDispatcher = userAdminCommandDispatcher;
            _userQueries = userQueries;
        }

        [AllowAnonymous]
        [ProducesResponseType(typeof(UserEntityDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiProblem), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
        [HttpPost("signup")]
        public async Task<IActionResult> UserRegister(UserDto newUserDetails)
        {
            if (newUserDetails == null)
                throw new ErrorCodeException(ErrorCodes.InvalidUserRegistrationDetails);

            var userRegistrationResult = await _userAdminCommandDispatcher.
                Dispatch<RegisterUserCommand, RegisterUserResult>(new RegisterUserCommand(newUserDetails.Email, newUserDetails.Password, newUserDetails.FirstName, newUserDetails.LastName));

            if (userRegistrationResult.InvaildEmail)
                throw new ErrorCodeException(ErrorCodes.InvaildEmail);

            if (userRegistrationResult.InvaildPassword)
                throw new ErrorCodeException(ErrorCodes.InvaildPassword);

            if (userRegistrationResult.UserAlreadyExist)
                throw new ErrorCodeException(ErrorCodes.UserAlreadyExist);

            return Ok(userRegistrationResult.User);
        }

        [AllowAnonymous]
        [ProducesResponseType(typeof(UserToken), (int)HttpStatusCode.OK)]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto loginRequest)
        {
            if (loginRequest == null)
                throw new ErrorCodeException(ErrorCodes.InvalidLoginRequest);

            var userLoginResult = await _userAdminCommandDispatcher.Dispatch<AuthenticateCommand, UserToken>
                (new AuthenticateCommand(loginRequest.Email, loginRequest.Password));

            return Ok(userLoginResult);
        }

        [RequiresAdminAccess]
        [ProducesResponseType(typeof(List<UserEntityDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
        [HttpGet("user/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("Invalid request");

            var user = await _userQueries.GetUserAsync(email);

            return Ok(user);
        }

        [ProducesResponseType(typeof(List<UserEntityDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userQueries.GetUsersAsync();
            return Ok(users);
        }

        [ProducesResponseType(typeof(UserEntityDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiProblem), (int)HttpStatusCode.Forbidden)]
        [HttpGet("user")]
        public async Task<IActionResult> GetUser()
        {
            var user = await _userQueries.GetUserAsync(GetUserId());
            return Ok(user);
        }

        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiProblem), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiProblem), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
        [HttpPut("user")]
        public async Task<IActionResult> UpdateUser(UserUpdateDto user)
        {
            var userUpdateUserResult = await _userAdminCommandDispatcher.
                Dispatch<UpdateUserCommand, UpdateUserResult>(new UpdateUserCommand(GetUserId(), user.Email, user.FirstName, user.LastName));

            if (userUpdateUserResult.UserNotFound)
                throw new ErrorCodeException(ErrorCodes.UserNotFound);

            if (userUpdateUserResult.EmailAlreadyExist)
                throw new ErrorCodeException(ErrorCodes.InvaildPassword);

            return Ok(userUpdateUserResult.IsSuccess);
        }

        [RequiresAdminAccess]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiProblem), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
        [HttpPost("access")]
        public async Task<IActionResult> GrantAccess(GrantAccessDto grantAccessDto)
        {
            var userUpdateUserResult = await _userAdminCommandDispatcher.
                Dispatch<GrantAccessCommand, GrantAccessResult>(new GrantAccessCommand(grantAccessDto.UserId, (UserAdministration.Domain.Enums.UserRole)grantAccessDto.UserRole));

            if (userUpdateUserResult.UserNotFound)
                throw new ErrorCodeException(ErrorCodes.UserNotFound);

            return Ok(userUpdateUserResult.IsSuccess);
        }
    }
}
