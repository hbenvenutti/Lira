using Lira.Common.Types;
using static BCrypt.Net.BCrypt;

namespace Lira.Common.Providers.Hash;

public class HashService : IHashService
{
    public string Hash(Password input)
    {
        return EnhancedHashPassword(input);
    }

    public bool Verify(Password text, string hash)
    {
        return EnhancedVerify(text, hash);
    }
}
