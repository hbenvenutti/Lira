namespace Lira.Common.Services.Hash;

public interface IHashService
{
    static abstract string Hash(string input);

    static abstract bool Verify(string input, string hash);
}
