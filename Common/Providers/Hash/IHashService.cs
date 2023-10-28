namespace Lira.Common.Providers.Hash;

public interface IHashService
{
    string Hash(string input);

    bool Verify(string input, string hash);
}
