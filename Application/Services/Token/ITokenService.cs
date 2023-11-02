using Lira.Domain.Authentication.Manager;

namespace Lira.Application.Services.Token;

public interface ITokenService
{
    string Sign(ManagerAuthDomain user);

    bool Verify(string token);
}
