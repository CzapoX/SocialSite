using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace Infrastructure.Security
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccesor;

        public UserAccessor(IHttpContextAccessor httpContextAccesor)
        {
            _httpContextAccesor = httpContextAccesor;
        }

        public string GetCurrentUserId()
        {
            var userId = _httpContextAccesor.HttpContext.User?
                .Claims?.FirstOrDefault(x =>
                x.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            return userId;
        }
    }
}
