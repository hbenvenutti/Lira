using BrazilianTypes.Types;

namespace Lira.Domain.Domains.Medium;

public interface IMediumRepository
{
    Task<MediumDomain> CreateAsync(MediumDomain domain);
    Task<MediumDomain> UpdateAsync(MediumDomain domain);
    Task DeleteAsync(MediumDomain domain);

    Task<MediumDomain?> FindByIdAsync(Guid id);
    Task<MediumDomain?> FindByCpfAsync(Cpf cpf);
}
