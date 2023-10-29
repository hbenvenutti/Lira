using Lira.Common.Types;

namespace Lira.Common.Providers.Hash;

public interface IHashService
{
    string Hash(Password input);

    bool Verify(Password input, string hash);
}
