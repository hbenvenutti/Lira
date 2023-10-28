namespace Lira.Common.Providers.Token;

public interface ITokenService
{
    string Sign(Guid userId);

    bool Verify(string token);
}
