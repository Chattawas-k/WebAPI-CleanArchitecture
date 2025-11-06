using System;
using System.Collections.Generic;
using System.Text;

namespace PlacementTest.Application.Features.LoginFeatures.Login
{
    public class LoginRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
