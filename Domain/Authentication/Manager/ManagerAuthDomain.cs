using Lira.Domain.Enums;

namespace Lira.Domain.Authentication.Manager;

public struct ManagerAuthDomain
{
    public Guid Id { get; init; }

    public string Username { get; init; }

    public DomainStatus Status { get; init; }
}
