using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Students;

namespace EUniManager.Persistence.Seed;

internal static class SeedHelper
{
    internal static string GetRandomString(int length)
    {
        if (length > 36)
        {
            length = 32;
        }

        string randomString = Guid.NewGuid().ToString().Substring(0, length).ToUpperInvariant();
        
        return randomString;
    }
}