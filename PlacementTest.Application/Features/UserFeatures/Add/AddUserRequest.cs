using MediatR;
using System;

namespace PlacementTest.Application.Features.UserFeatures.Add
{
    public sealed record class AddUserRequest(
        string UserName,
        string Email,
        string Password,
        string? Role // Optional: assign a role at registration
    ) : IRequest<AddUserResponse>;
}