using Microsoft.AspNetCore.Http;
using PlacementTest.Application.Common.Services;
using System.Security.Claims;

namespace PlacementTest.Persistence.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? UserId
        {
            get
            {
                var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (Guid.TryParse(userId, out var guid))
                    return guid;
                return null;
            }
        }
    }
}
