namespace Lira.Common.Providers.HashProvider;

public interface IHashProvider
{
    string Hash(string input);

    bool Verify(string input, string hash);
}
