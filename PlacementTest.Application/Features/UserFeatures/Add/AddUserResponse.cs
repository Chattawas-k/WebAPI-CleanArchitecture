using System;

namespace PlacementTest.Application.Features.UserFeatures.Add
{
    public sealed record class AddUserResponse
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }
}