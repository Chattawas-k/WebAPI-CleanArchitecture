using MediatR;
using PlacementTest.Application.Common.Services;

namespace PlacementTest.Application.Features.Auth.RefreshTokenFeatures.RefreshToken
{
 public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, RefreshTokenResponse>
 {
 private readonly ILoginService _loginService;

 public RefreshTokenHandler(ILoginService loginService)
 {
 _loginService = loginService;
 }

 public async Task<RefreshTokenResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
 {
 return await _loginService.RefreshTokenAsync(request);
 }
 }
}
