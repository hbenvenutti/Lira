using Lira.Common.Types;

namespace Lira.Domain.Domains.Person;

public interface IPersonRepository
{
    # region ---- write --------------------------------------------------------

    public Task<PersonDomain> CreateAsync(PersonDomain personDomain);
    public Task<PersonDomain> UpdateAsync(PersonDomain personDomain);
    public Task DeleteAsync(PersonDomain personDomain);

    # endregion

    # region ---- read ---------------------------------------------------------

    public Task<PersonDomain?> FindByCpfAsync(Cpf cpf);

    # endregion
}
