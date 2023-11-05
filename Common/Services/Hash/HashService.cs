using static BCrypt.Net.BCrypt;

namespace Lira.Common.Services.Hash;

public class HashService : IHashService
{
    public static string Hash(string input)
    {
        return EnhancedHashPassword(input);
    }

    public static bool Verify(string text, string hash)
    {
        return EnhancedVerify(text, hash);
    }
}
