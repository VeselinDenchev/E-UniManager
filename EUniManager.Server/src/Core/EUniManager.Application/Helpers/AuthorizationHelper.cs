using System.Security.Claims;

using EUniManager.Domain.Enums;

using Microsoft.AspNetCore.Http;

namespace EUniManager.Application.Helpers;

internal static class AuthorizationHelper
{
    internal static Guid GetCurrentUserId(IHttpContextAccessor httpContextAccessor)
    {
        var userClaimsPrincipal = httpContextAccessor?.HttpContext?.User;
        var userIdAsString = userClaimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        bool isParsed = Guid.TryParse(userIdAsString, out Guid userId);
        if (!isParsed) throw new ArgumentException("Invalid user id!");

        return userId;
    }

    internal static UserRole GetCurrentUserRole(IHttpContextAccessor httpContextAccessor)
    {
        var userClaimsPrincipal = httpContextAccessor?.HttpContext?.User;

        if (userClaimsPrincipal is null || userClaimsPrincipal.Identity?.IsAuthenticated == false)
        {
            throw new ArgumentException("Invalid user role!");
        }

        string roleClaimValue = userClaimsPrincipal.Claims.Where(c => c.Type == ClaimTypes.Role)
                                                          .Select(c => c.Value)
                                                          .FirstOrDefault() ??
                                throw new ArgumentException("Invalid user role!");

        bool isParsed = Enum.TryParse(roleClaimValue, out UserRole role);
        if (!isParsed) throw new ArgumentException("Invalid user role!");

        return role;
    }
}