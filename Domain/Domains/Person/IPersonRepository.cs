using BrazilianTypes.Types;

namespace Lira.Domain.Domains.Person;

public interface IPersonRepository
{
    # region ---- write --------------------------------------------------------

    public Task<PersonDomain> CreateAsync(PersonDomain personDomain);
    public Task<PersonDomain> UpdateAsync(PersonDomain personDomain);
    public Task DeleteAsync(PersonDomain personDomain);

    # endregion

    # region ---- read ---------------------------------------------------------

    public Task<PersonDomain?> FindByCpfAsync(
        Cpf cpf,
        bool includeDeleted = false,
        bool includeOrixas = false,
        bool includeEmails = false,
        bool includePhones = false,
        bool includeAddresses = false,
        bool includeMedium = false,
        bool includeManager = false
    );

    public Task<PersonDomain?> FindByIdAsync(
        Guid id,
        bool includeDeleted = false,
        bool includeOrixas = false,
        bool includeEmails = false,
        bool includePhones = false,
        bool includeAddresses = false,
        bool includeMedium = false,
        bool includeManager = false
    );

    # endregion
}
