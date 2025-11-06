using MediatR;
using PlacementTest.Application.Features.Auth.LoginFeatures.Login;

namespace PlacementTest.Application.Features.Auth.LoginFeatures.AzureAd
{
 public class AzureAdLoginRequest : IRequest<LoginResponse>
 {
 public required string AccessToken { get; set; }
 }
}
