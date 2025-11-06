using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlacementTest.Application.Common.Services;
using PlacementTest.Application.Features.LoginFeatures.Login;
using PlacementTest.Application.Features.RefreshTokenFeatures.RefreshToken;
using PlacementTest.Application.Features.UserFeatures.Add;
using PlacementTest.WebAPI.Controllers.Base;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace PlacementTest.WebAPI.Controllers
{
    public class AuthController : BaseController
    {
        private readonly ILoginService _loginService;

        public AuthController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _loginService.LoginAsync(request);
            if (response == null)
                return Unauthorized();

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AddUserRequest request, [FromServices] IMediator mediator)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var response = await _loginService.RefreshTokenAsync(request);
            if (response == null)
                return Unauthorized();
            return Ok(response);
        }

        [HttpPost("azure-ad-login")]
        [AllowAnonymous]
        public async Task<IActionResult> AzureAdLogin([FromBody] AzureAdLoginRequest request)
        {
            // Validate Azure AD token
            var principal = await _loginService.ValidateAzureAdTokenAsync(request.AccessToken);
            if (principal == null)
                return Unauthorized();

            // Extract email or unique identifier from claims
            var email = principal.FindFirst(ClaimTypes.Email)?.Value ?? principal.FindFirst("preferred_username")?.Value;
            if (string.IsNullOrEmpty(email))
                return Unauthorized();

            // Find or create local user
            var user = await _loginService.FindOrCreateExternalUserAsync(email);

            // Get roles
            var roles = await _loginService.GetUserRolesAsync(user);

            // Issue your own JWT/refresh tokens
            var token = _loginService.GenerateJwtToken(user, roles);
            var refreshToken = _loginService.GenerateRefreshToken(user);

            return Ok(new LoginResponse
            {
                Token = token,
                RefreshToken = refreshToken,
                UserName = user.UserName,
                Roles = roles
            });
        }
        }

    public class AzureAdLoginRequest
    {
        public required string AccessToken { get; set; }
    }
}
