using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace PlacementTest.Application.Features.Auth.RefreshTokenFeatures.RefreshToken
{
    public class RefreshTokenRequest : IRequest<RefreshTokenResponse>
    {
        public required string RefreshToken { get; set; }
    }
}
