using static BCrypt.Net.BCrypt;

namespace Lira.Common.Providers.Hash;

public class HashService : IHashService
{
    public string Hash(string input)
    {
        return EnhancedHashPassword(input);
    }

    public bool Verify(string text, string hash)
    {
        return EnhancedVerify(text, hash);
    }
}
