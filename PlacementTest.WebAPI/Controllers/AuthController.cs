using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlacementTest.Application.Common.Services;
using PlacementTest.Application.Features.UserFeatures.Add;
using PlacementTest.WebAPI.Controllers.Base;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using PlacementTest.Application.Features.Auth.LoginFeatures.AzureAd;
using PlacementTest.Application.Features.Auth.LoginFeatures.Login;
using PlacementTest.Application.Features.Auth.RefreshTokenFeatures.RefreshToken;

namespace PlacementTest.WebAPI.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _mediator.Send(request);
            if (response == null)
                return Unauthorized();

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AddUserRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var response = await _mediator.Send(request);
            if (response == null)
                return Unauthorized();
            return Ok(response);
        }

        [HttpPost("azure-ad-login")]
        [AllowAnonymous]
        public async Task<IActionResult> AzureAdLogin([FromBody] AzureAdLoginRequest request)
        {
            var response = await _mediator.Send(request);
            if (response == null)
                return Unauthorized();
            return Ok(response);
        }
    }

    public class AzureAdLoginRequestDto
    {
        public required string AccessToken { get; set; }
    }
}
