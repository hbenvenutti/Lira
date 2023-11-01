namespace Lira.Domain.Domains.Medium;

public interface IMediumRepository
{
    Task<MediumDomain> CreateAsync(MediumDomain domain);
    Task<MediumDomain> UpdateAsync(MediumDomain domain);
    Task DeleteAsync(MediumDomain domain);
}
