using System;
using System.Collections.Generic;
using System.Text;

namespace PlacementTest.Application.Features.LoginFeatures.Login
{
    public class LoginResponse
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public string? UserName { get; set; }
        public IList<string>? Roles { get; set; }
    }
}
