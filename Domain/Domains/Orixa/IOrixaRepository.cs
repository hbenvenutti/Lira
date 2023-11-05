namespace Lira.Domain.Domains.Orixa;

public interface IOrixaRepository
{
    Task<OrixaDomain?> FindByIdAsync(Guid id);
}
