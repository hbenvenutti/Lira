using static BCrypt.Net.BCrypt;

namespace Lira.Common.Providers.HashProvider;

public class HashProvider : IHashProvider
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
