using Microsoft.AspNetCore.Identity;

namespace EUniManager.Application.Extensions;

public static class IdentityResultExtensions
{
    public static void ThrowExceptionIfFailed(this IdentityResult identityResult)
    {
        if (identityResult.Succeeded) return;

        string message = string.Join(Environment.NewLine, identityResult.Errors);
        throw new ArgumentException(message);
    }
}