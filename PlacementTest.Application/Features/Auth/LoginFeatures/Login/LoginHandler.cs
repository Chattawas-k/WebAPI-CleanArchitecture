using MediatR;
using PlacementTest.Application.Common.Services;

namespace PlacementTest.Application.Features.Auth.LoginFeatures.Login
{
    public class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly ILoginService _loginService;

        public LoginHandler(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            return await _loginService.LoginAsync(request);
        }
    }
}
