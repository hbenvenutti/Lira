using BrazilianTypes.Types;
using Lira.Data.Contexts;
using Lira.Data.Entities;
using Lira.Data.Enums;
using Lira.Data.Extensions;
using Lira.Domain.Domains.Person;
using Microsoft.EntityFrameworkCore;

namespace Lira.Data.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly IDbContext _dbContext;

    # region ---- constructor --------------------------------------------------

    public PersonRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    # endregion

    # region ---- write --------------------------------------------------------

    public async Task<PersonDomain> CreateAsync(PersonDomain personDomain)
    {
        var person = (PersonEntity) personDomain;

        await _dbContext.People.AddAsync(person);

        await _dbContext.SaveChangesAsync();

        return (PersonDomain) person;
    }

    public async Task<PersonDomain> UpdateAsync(PersonDomain personDomain)
    {
        var person = (PersonEntity) personDomain;

        _dbContext.People.Update(person);

        await _dbContext.SaveChangesAsync();

        return (PersonDomain) person;
    }

    public async Task DeleteAsync(PersonDomain personDomain)
    {
        var person = (PersonEntity) personDomain;

        person.DeleteEntity();

        _dbContext.People.Update(person);

        await _dbContext.SaveChangesAsync();
    }

    # endregion

    # region ---- find by id ---------------------------------------------------

    public async Task<PersonDomain?> FindByIdAsync(
        Guid id,
        bool includeDeleted = false,
        bool includeOrixas = false,
        bool includeEmails = false,
        bool includePhones = false,
        bool includeAddresses = false,
        bool includeMedium = false,
        bool includeManager = false
    )
    {
        var query = _dbContext.People
            .AsNoTracking();

        if (!includeDeleted)
        {
            query = query
                .Where(person => person.Status != EntityStatus.Deleted);
        }

        if (includeOrixas)
        {
            query = query
                .AsNoTracking()
                .Include(person => person.PersonOrixas)
                .Include(person => person.PersonOrixas!
                    .Select(personOrixa => personOrixa.Orixa)
                );
        }

        if (includeEmails)
        {
            query = query
                .AsNoTracking()
                .Include(person => person.Emails);
        }

        if (includePhones)
        {
            query = query
                .AsNoTracking()
                .Include(person => person.Phones);
        }

        if (includeAddresses)
        {
            query = query
                .AsNoTracking()
                .Include(person => person.Addresses);
        }

        if (includeMedium)
        {
            query = query
                .AsNoTracking()
                .Include(person => person.Medium);
        }

        if (includeManager)
        {
            query = query
                .AsNoTracking()
                .Include(person => person.Manager);
        }

        var person = await query
            .SingleOrDefaultAsync(person => person.Id == id);

        return person is null
            ? null
            : (PersonDomain) person;
    }

    # endregion

    # region ---- find by cpf --------------------------------------------------

    public async Task<PersonDomain?> FindByCpfAsync(
        Cpf cpf,
        bool includeDeleted = false,
        bool includeOrixas = false,
        bool includeEmails = false,
        bool includePhones = false,
        bool includeAddresses = false,
        bool includeMedium = false,
        bool includeManager = false
    )
    {
        var query = _dbContext.People
            .AsNoTracking();

        if (!includeDeleted)
        {
            query = query
                .Where(person => person.Status != EntityStatus.Deleted);
        }

        if (includeOrixas)
        {
            query = query
                .AsNoTracking()
                .Include(person => person.PersonOrixas)
                .Include(person => person.PersonOrixas!
                    .Select(personOrixa => personOrixa.Orixa)
                );
        }

        if (includeEmails)
        {
            query = query
                .AsNoTracking()
                .Include(person => person.Emails);
        }

        if (includePhones)
        {
            query = query
                .AsNoTracking()
                .Include(person => person.Phones);
        }

        if (includeAddresses)
        {
            query = query
                .AsNoTracking()
                .Include(person => person.Addresses);
        }

        if (includeMedium)
        {
            query = query
                .AsNoTracking()
                .Include(person => person.Medium);
        }

        if (includeManager)
        {
            query = query
                .AsNoTracking()
                .Include(person => person.Manager);
        }

        var person = await query
            .SingleOrDefaultAsync(person => person.Cpf == cpf);

        return person is null
            ? null
            : (PersonDomain) person;
    }

    # endregion
}
